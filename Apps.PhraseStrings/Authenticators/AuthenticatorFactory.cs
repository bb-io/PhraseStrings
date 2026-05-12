using Apps.PhraseStrings.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp.Authenticators;

namespace Apps.PhraseStrings.Authenticators;

public static class AuthenticatorFactory
{
    public static IAuthenticator Create(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var credsList = creds.ToList();
        string connectionType = credsList.Get(CredsNames.ConnectionType).Value;

        return connectionType switch
        {
            ConnectionTypes.AccessToken => new AccessTokenAuthenticator(credsList),
            ConnectionTypes.ApiToken => new ApiTokenAuthenticator(credsList),
            _ => throw new Exception($"Unknown connection type was passed to AuthenticatorFactory: {connectionType}")
        };
    }
}