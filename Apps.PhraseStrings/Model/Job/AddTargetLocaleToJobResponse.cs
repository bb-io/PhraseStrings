using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job;

public class AddTargetLocaleToJobResponse
{
    [Display("Job locale ID")]
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("job")]
    public JobInfo Job { get; set; } = new();

    [Display("Users assigned")]
    [JsonProperty("users")]
    public IEnumerable<UserInfo> Users { get; set; } = [];

    [Display("Users teams")]
    [JsonProperty("teams")]
    public IEnumerable<TeamInfo> Teams { get; set; } = [];

    [JsonProperty("locale")]
    public LocaleInfo Locale { get; set; } = new();

    [Display("Is this locale completed for a job?")]
    [JsonProperty("completed")]
    public bool Completed { get; set; }

    [Display("Translation completed at")]
    [JsonProperty("translation_completed_at")]
    public DateTime? TranslationCompletedAt { get; set; }

    [Display("Review completed at")]
    [JsonProperty("review_completed_at")]
    public DateTime? ReviewCompletedAt { get; set; }
}

public class JobInfo
{
    [Display("Job ID")]
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [Display("Job name")]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [Display("Job state")]
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;
}

public class TeamInfo
{
    [Display("Team ID")]
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [Display("Team name")]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [Display("Team role")]
    [JsonProperty("role")]
    public string Role { get; set; } = string.Empty;
}

