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
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.Token) { DisplayName = "Access token", Sensitive = true},
                new(CredsNames.Url){ DisplayName = "Base URL",
                Description = "Select the base URL according to your Phrase Strings data center",
                DataItems =
                    [
                        new("https://api.phrase.com", "EU data center"),
                        new("https://api.us.app.phrase.com","US data center"),
                        new("https://api.phrase-staging.com","EU staging data center")
                    ]}
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values) => values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)
        ).ToList();
}