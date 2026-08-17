using Apps.PhraseStrings.Authenticators;
using Apps.PhraseStrings.Constants;
using System.Net;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RestSharp;

namespace Apps.PhraseStrings.Api;

public class PhraseStringsClient(IEnumerable<AuthenticationCredentialsProvider> creds) : BlackBirdRestClient(new()
{
    BaseUrl = new Uri(creds.Get(CredsNames.Url).Value),
    MaxTimeout = 180000,
    Authenticator = AuthenticatorFactory.Create(creds)
})
{
    private readonly AsyncRetryPolicy<RestResponse> _retryPolicy = Policy
        .HandleResult<RestResponse>(response => response.StatusCode == HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(RateLimits.RetryCount, (retryAttempt, result, _) => GetRetryDelay(retryAttempt, result.Result),
            (_, _, _, _) => Task.CompletedTask);

    protected override Exception ConfigureErrorException(RestResponse response)
        => CreateErrorException(response);

    public async Task<List<TItem>> Paginate<TItem>(RestRequest originalRequest, int pageSize = 50)
    {
        var allItems = new List<TItem>();
        var endpoint = originalRequest.Resource;
        var method = originalRequest.Method;
        int page = 1; 

        while (true)
        {
            var pageRequest = new RestRequest(endpoint, method);

            foreach (var param in originalRequest.Parameters.Where(x => x.Type == ParameterType.QueryString))
            {
                if (string.IsNullOrEmpty(param.Name))
                    continue;

                pageRequest.AddQueryParameter(param.Name, param.Value?.ToString());
            }

            pageRequest.AddQueryParameter("page", page.ToString());
            pageRequest.AddQueryParameter("per_page", pageSize.ToString());

            var items = await ExecuteWithErrorHandling<List<TItem>>(pageRequest);

            if (items != null && items.Any())
            {
                allItems.AddRange(items);
            }
            else
            {
                break;
            }

            page++;
        }

        return allItems;
    }
    
    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        string content = (await ExecuteWithErrorHandling(request)).Content ?? string.Empty;

        T val = JsonConvert.DeserializeObject<T>(content, JsonSettings ?? new())
            ?? throw new Exception($"Could not parse {content} to {typeof(T)}");

        return val;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        RestResponse restResponse = await ExecuteAsync(request);
        if (!restResponse.IsSuccessStatusCode)
        {
            throw ConfigureErrorException(restResponse);
        }

        return restResponse;
    }
    
    public new Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default)
        => _retryPolicy.ExecuteAsync(() => base.ExecuteAsync(request, cancellationToken));

    internal Exception CreateErrorException(RestResponse response)
    {
        var content = response.Content ?? string.Empty;

        if (response.ContentType?.Contains("text/html", StringComparison.OrdinalIgnoreCase) == true || content.TrimStart().StartsWith('<'))
        {
            return new PluginApplicationException(ExtractHtmlErrorMessage(content));
        }

        if (TryFormatErrorMessage(content, response.StatusCode, out var errorMessage))
        {
            return new PluginApplicationException(errorMessage);
        }

        return new PluginApplicationException(
            string.IsNullOrWhiteSpace(content) ? "Error when running a request" : content);
    }
    
    internal static TimeSpan GetRetryDelay(int retryAttempt, RestResponse? response)
    {
        var retryAfter = GetRetryAfterDelay(response);
        if (retryAfter.HasValue)
        {
            return Clamp(retryAfter.Value);
        }

        var quotaReset = GetExhaustedQuotaResetDelay(response);
        if (quotaReset.HasValue)
        {
            return Clamp(quotaReset.Value + GetJitter(quotaReset.Value.TotalSeconds));
        }

        var isConcurrency = IsConcurrencyLimit(response);
        var baseSeconds = isConcurrency ? RateLimits.ConcurrencyBaseBackoffSeconds : RateLimits.QuotaBaseBackoffSeconds;
        var maxSeconds = isConcurrency ? RateLimits.ConcurrencyMaxBackoffSeconds : RateLimits.QuotaMaxBackoffSeconds;

        var backoffSeconds = Math.Min(baseSeconds * Math.Pow(2, retryAttempt - 1), maxSeconds);

        return Clamp(TimeSpan.FromSeconds(backoffSeconds) + GetJitter(backoffSeconds));
    }

    private static TimeSpan? GetRetryAfterDelay(RestResponse? response)
    {
        var retryAfter = GetHeaderValue(response, RateLimits.RetryAfterHeader);
        if (string.IsNullOrWhiteSpace(retryAfter))
        {
            return null;
        }

        if (int.TryParse(retryAfter, out var seconds))
        {
            return seconds > 0 ? TimeSpan.FromSeconds(seconds) : null;
        }

        if (DateTimeOffset.TryParse(retryAfter, out var retryAt))
        {
            var delay = retryAt - DateTimeOffset.UtcNow;
            return delay > TimeSpan.Zero ? delay : null;
        }

        return null;
    }

    private static TimeSpan? GetExhaustedQuotaResetDelay(RestResponse? response)
    {
        var remaining = GetHeaderValue(response, RateLimits.RateLimitRemainingHeader)
            ?? GetHeaderValue(response, RateLimits.TmsRateLimitRemainingHeader);
        if (!int.TryParse(remaining, out var remainingRequests) || remainingRequests > 0)
        {
            return null;
        }

        var reset = GetHeaderValue(response, RateLimits.RateLimitResetHeader)
            ?? GetHeaderValue(response, RateLimits.TmsRateLimitResetHeader);
        if (!long.TryParse(reset, out var resetUnixSeconds))
        {
            return null;
        }

        var delay = DateTimeOffset.FromUnixTimeSeconds(resetUnixSeconds) - DateTimeOffset.UtcNow;
        return delay > TimeSpan.Zero ? delay : null;
    }

    private static bool IsConcurrencyLimit(RestResponse? response)
        => response?.Content?.Contains(RateLimits.ConcurrencyLimitMarker, StringComparison.OrdinalIgnoreCase) == true;

    private static string? GetHeaderValue(RestResponse? response, string name)
        => response?.Headers?
            .FirstOrDefault(header => string.Equals(header.Name, name, StringComparison.OrdinalIgnoreCase))?
            .Value?.ToString()?.Trim();

    private static TimeSpan GetJitter(double delaySeconds)
        => TimeSpan.FromMilliseconds(Random.Shared.Next(0,
            (int)Math.Max(RateLimits.MinJitterMilliseconds, delaySeconds * 100)));

    private static TimeSpan Clamp(TimeSpan delay)
        => delay > TimeSpan.FromSeconds(RateLimits.MaxRetryDelaySeconds)
            ? TimeSpan.FromSeconds(RateLimits.MaxRetryDelaySeconds)
            : delay;

    private static string ExtractHtmlErrorMessage(string html)
    {
        if (string.IsNullOrEmpty(html)) return "N/A";

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");
        var bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

        var title = titleNode?.InnerText.Trim() ?? "No Title";
        var body = bodyNode?.InnerText.Trim() ?? "No Description";
        return $"{title}: \nError Description: {body}";
    }

    private static bool TryFormatErrorMessage(string content, HttpStatusCode statusCode, out string message)
    {
        message = string.Empty;

        JObject? response = null;

        if (!string.IsNullOrWhiteSpace(content))
        {
            try
            {
                response = JObject.Parse(content);
            }
            catch (JsonReaderException)
            {
                // Keep processing so status-code-specific messages can still be used.
            }
        }

        if (response?.GetValue("errors", StringComparison.OrdinalIgnoreCase) is JArray { Count: > 0 } errors)
        {
            var formattedErrors = errors
                .OfType<JObject>()
                .Select(FormatValidationError)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (formattedErrors.Count > 0)
            {
                message = string.Join(Environment.NewLine, formattedErrors);
                return true;
            }
        }

        if (statusCode == HttpStatusCode.NotFound)
        {
            message = "Phrase Strings could not find the requested item. Check that the supplied IDs are correct and that the connected account has access to it.";
            return true;
        }

        var topLevelMessage = response is null ? null : GetNonEmptyString(response, "message");

        if (statusCode == HttpStatusCode.TooManyRequests)
        {
            var reported = string.IsNullOrWhiteSpace(topLevelMessage) ? "Too many requests." : FormatSentence(topLevelMessage);
            message = $"Phrase Strings is rejecting requests because an API limit of the connected account was reached: {reported} " +
                      "The request was retried with backoff and the limit was still in place. Reduce the number of Phrase Strings " +
                      "requests running at the same time, increase the interval of polling events, or ask Phrase support to raise the account limits.";
            return true;
        }

        if (topLevelMessage is null)
        {
            return false;
        }

        message = FormatSentence(topLevelMessage);
        return true;
    }

    private static string FormatValidationError(JObject error)
    {
        var resource = GetNonEmptyString(error, "resource");
        var field = GetNonEmptyString(error, "field")?.Replace('_', ' ');
        var errorMessage = GetNonEmptyString(error, "message");

        var result = string.Join(" ", new[] { resource, field, errorMessage }
            .Where(x => !string.IsNullOrWhiteSpace(x)));

        if (string.IsNullOrEmpty(result))
        {
            return string.Empty;
        }

        return FormatSentence(result);
    }

    private static string FormatSentence(string value)
    {
        var result = char.ToUpperInvariant(value[0]) + value[1..];
        return result.EndsWith('.') || result.EndsWith('!') || result.EndsWith('?')
            ? result
            : $"{result}.";
    }

    private static string? GetNonEmptyString(JObject source, string propertyName)
    {
        var value = source.GetValue(propertyName, StringComparison.OrdinalIgnoreCase);
        return value?.Type == JTokenType.String && !string.IsNullOrWhiteSpace(value.Value<string>())
            ? value.Value<string>()!.Trim()
            : null;
    }
}
