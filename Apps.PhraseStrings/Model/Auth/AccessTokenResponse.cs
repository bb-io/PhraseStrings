using System.Text.Json.Serialization;

namespace Apps.PhraseStrings.Model.Auth;

public class AccessTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}