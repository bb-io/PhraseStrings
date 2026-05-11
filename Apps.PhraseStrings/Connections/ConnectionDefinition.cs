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
            Name = "Access token",
            DisplayName = "Access Token",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.Token) { DisplayName = "API Token", Sensitive = true },
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
            }
        },
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values) => values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)
        ).ToList();
}