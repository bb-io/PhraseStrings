using Apps.PhraseStrings.Authenticators;
using Apps.PhraseStrings.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using HtmlAgilityPack;
using Newtonsoft.Json;
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
    {
        var error = JsonConvert.DeserializeObject(response.Content ?? string.Empty);

        if (response.ContentType?.Contains("text/html", StringComparison.OrdinalIgnoreCase) == true || response.Content?.StartsWith('<') == true)
        {
            var errorMessage = ExtractHtmlErrorMessage(response.Content ?? string.Empty);
            throw new PluginApplicationException(errorMessage);
        }

        throw new PluginApplicationException(error?.ToString() ?? "Error when running a request");
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
}