using Apps.PhraseStrings.Constants;
using Apps.PhraseStrings.Model.Auth;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;
using RestSharp.Authenticators;

namespace Apps.PhraseStrings.Authenticators;

public class ApiTokenAuthenticator(IEnumerable<AuthenticationCredentialsProvider> creds) : IAuthenticator
{
    public async ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        string euTokenUrl = "https://eu.phrase.com/idm/oauth/token";
        string usTokenUrl = "https://us.phrase.com/idm/oauth/token";
        
        string baseUrl = creds.Get(CredsNames.Url).Value;
        string oauthBaseUrl = baseUrl switch
        {
            not null when baseUrl.StartsWith(Urls.Eu.TrimEnd('/')) => euTokenUrl,
            not null when baseUrl.StartsWith(Urls.EuStaging.TrimEnd('/')) => euTokenUrl,
            not null when baseUrl.StartsWith(Urls.Us.TrimEnd('/')) => usTokenUrl,
            _ => throw new Exception($"Unsupported base URL for API token exchange: {baseUrl}")
        };

        var apiToken = creds.Get(CredsNames.Token).Value;
        using var tokenClient = new RestClient();
        
        var tokenRequest = new RestRequest(oauthBaseUrl, Method.Post);
        tokenRequest.AddParameter("grant_type", "urn:ietf:params:oauth:grant-type:token-exchange", ParameterType.GetOrPost);
        tokenRequest.AddParameter("subject_token", apiToken, ParameterType.GetOrPost);
        tokenRequest.AddParameter("subject_token_type", "urn:phrase:params:oauth:token-type:api_token", ParameterType.GetOrPost);
        tokenRequest.AddParameter("requested_token_type", "urn:ietf:params:oauth:token-type:access_token", ParameterType.GetOrPost);
        
        var response = await tokenClient.ExecuteAsync<AccessTokenResponse>(tokenRequest);
        if (response.Data?.AccessToken == null)
            throw new PluginApplicationException("No token returned from login response.");
        
        request.AddOrUpdateHeader("Authorization", $"Bearer {response.Data.AccessToken}");
    }
}