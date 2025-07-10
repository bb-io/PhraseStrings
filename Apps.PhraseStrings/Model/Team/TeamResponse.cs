using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Team;

public class TeamResponse
{
    [JsonProperty("id")]
    [Display("Team ID")]
    public string TeamId { get; set; } = string.Empty;

    [JsonProperty("name")]
    [Display("Team name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("projects")]
    [Display("Projects linked to team")]
    public List<ProjectInfo> Projects { get; set; } = [];

    [JsonProperty("spaces")]
    [Display("Spaces linked to team")]
    public List<SpaceInfo> Spaces { get; set; } = [];

    [JsonProperty("users")]
    [Display("Users in a team")]
    public List<UserInfo> Users { get; set; } = [];
}

public class SpaceInfo
{
    [JsonProperty("id")]
    [Display("Space ID")]
    public string Id { get; set; }

    [JsonProperty("name")]
    [Display("Space name")]
    public string Name { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("projects_count")]
    [Display("Number of projects in space")]
    public int ProjectsCount { get; set; }
}
