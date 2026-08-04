using Apps.PhraseStrings.Authenticators;
using Apps.PhraseStrings.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.PhraseStrings.Api;

public class PhraseStringsClient(IEnumerable<AuthenticationCredentialsProvider> creds) : BlackBirdRestClient(new()
{
    BaseUrl = new Uri(creds.Get(CredsNames.Url).Value),
    MaxTimeout = 180000,
    Authenticator = AuthenticatorFactory.Create(creds)
})
{
    protected override Exception ConfigureErrorException(RestResponse response)
        => CreateErrorException(response);

    internal Exception CreateErrorException(RestResponse response)
    {
        var content = response.Content ?? string.Empty;

        if (response.ContentType?.Contains("text/html", StringComparison.OrdinalIgnoreCase) == true || content.TrimStart().StartsWith('<'))
        {
            return new PluginApplicationException(ExtractHtmlErrorMessage(content));
        }

        if (TryFormatValidationErrors(content, out var validationMessage))
        {
            return new PluginApplicationException(validationMessage);
        }

        return new PluginApplicationException(
            string.IsNullOrWhiteSpace(content) ? "Error when running a request" : content);
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

    private static bool TryFormatValidationErrors(string content, out string message)
    {
        message = string.Empty;

        if (string.IsNullOrWhiteSpace(content))
        {
            return false;
        }

        try
        {
            var response = JObject.Parse(content);
            if (response.GetValue("errors", StringComparison.OrdinalIgnoreCase) is not JArray { Count: > 0 } errors)
            {
                return false;
            }

            var formattedErrors = errors
                .OfType<JObject>()
                .Select(FormatValidationError)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (formattedErrors.Count == 0)
            {
                return false;
            }

            message = string.Join(Environment.NewLine, formattedErrors);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
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

        result = char.ToUpperInvariant(result[0]) + result[1..];
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
