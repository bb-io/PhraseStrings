using Apps.PhraseStrings.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.PhraseStrings.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = ConnectionTypes.AccessToken,
            DisplayName = "Access token",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.Token) { DisplayName = "Access token", Sensitive = true },
                new(CredsNames.Url)
                { 
                    DisplayName = "Base URL",
                    Description = "Select the base URL according to your Phrase Strings data center",
                    DataItems =
                    [
                        new(Urls.Eu, "EU data center"),
                        new(Urls.Us, "US data center"),
                        new(Urls.EuStaging, "EU staging data center")
                    ]
                }
            },
        },
        new()
        {
            Name = ConnectionTypes.ApiToken,
            DisplayName = "Platform API token (recommended)",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.Token) { DisplayName = "Access token", Sensitive = true },
                new(CredsNames.Url)
                { 
                    DisplayName = "Base URL",
                    Description = "Select the base URL according to your Phrase Strings data center",
                    DataItems =
                    [
                        new(Urls.Eu, "EU data center"),
                        new(Urls.Us, "US data center"),
                        new(Urls.EuStaging, "EU staging data center")
                    ]
                }
            },
        },
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var providers = values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)).ToList();
        var connectionType = values[nameof(ConnectionPropertyGroup)] switch
        {
            var ct when ConnectionTypes.SupportedConnectionTypes.Contains(ct) => ct,
            _ => throw new Exception($"Unknown connection type: {values[nameof(ConnectionPropertyGroup)]}")
        };

        providers.Add(new AuthenticationCredentialsProvider(CredsNames.ConnectionType, connectionType));
        return providers;
    }
}