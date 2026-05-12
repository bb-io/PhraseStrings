using Apps.PhraseStrings.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;
using RestSharp.Authenticators;

namespace Apps.PhraseStrings.Authenticators;

public class AccessTokenAuthenticator(IEnumerable<AuthenticationCredentialsProvider> creds) : IAuthenticator
{
    public ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        request.AddOrUpdateHeader("Authorization", $"token {creds.Get(CredsNames.Token).Value}");
        return ValueTask.CompletedTask;
    }
}