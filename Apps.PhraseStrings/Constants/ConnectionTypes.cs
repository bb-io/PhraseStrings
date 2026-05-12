namespace Apps.PhraseStrings.Constants;

public static class ConnectionTypes
{
    public const string AccessToken = "Access token";
    public const string ApiToken = "API token";

    public static readonly IEnumerable<string> SupportedConnectionTypes = [AccessToken, ApiToken];
}