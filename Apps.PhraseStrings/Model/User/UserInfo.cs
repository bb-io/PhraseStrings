using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
namespace Apps.PhraseStrings.Model.User;
public class UserInfo
{
    [Display("User ID")]
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [Display("User login (username)")]
    [JsonProperty("username")]
    public string Username { get; set; } = string.Empty;

    [Display("User name")]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [Display("User role")]
    [JsonProperty("role")]
    public string Role { get; set; } = string.Empty;
}
