using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Team;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.User;

public class UserResponse
{
    [JsonProperty("id")]
    [Display("User ID")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("email")]
    [Display("Email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("username")]
    [Display("Username")]
    public string Username { get; set; } = string.Empty;

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("last_activity_at")]
    [Display("Last activity at")]
    public DateTime? LastActivityAt { get; set; }

    [JsonProperty("role")]
    [Display("Role")]
    public string Role { get; set; } = string.Empty;

    [JsonProperty("projects")]
    [Display("Projects")]
    public List<UserProject>? Projects { get; set; } = [];

    [JsonProperty("default_locale_codes")]
    [Display("Default locale codes")]
    public List<string> DefaultLocaleCodes { get; set; } = [];

    [JsonProperty("teams")]
    [Display("Teams")]
    public List<UserTeam> Teams { get; set; } = [];

    [JsonProperty("spaces")]
    [Display("Spaces")]
    public List<SpaceInfo> Spaces { get; set; } = [];
}

public class UserProject
{
    [JsonProperty("id")]
    [Display("Project ID")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    [Display("Project name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("main_format")]
    [Display("Main format")]
    public string MainFormat { get; set; } = string.Empty;

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("locales")]
    [Display("Locales")]
    public List<LocaleInfo> Locales { get; set; } = [];
}

public class UserTeam
{
    [JsonProperty("id")]
    [Display("Team ID")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    [Display("Team name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }
}
