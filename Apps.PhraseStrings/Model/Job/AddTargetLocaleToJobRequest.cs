using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job;

public class AddTargetLocaleToJobRequest
{
    [JsonProperty("locale_id")]
    [Display("Locale ID")]
    [DataSource(typeof(LocaleDataHandler))]
    public string LocaleId { get; set; } = string.Empty;

    [JsonProperty("branch")]
    [Display("Branch name")]
    [DataSource(typeof(BranchDataHandler))]
    public string? Branch { get; set; }

    [JsonProperty("user_ids")]
    [Display("Translator IDs to be assigned")]
    [DataSource(typeof(UserDataHandler))]
    public IEnumerable<string>? TranslatorUserIds { get; set; }

    [JsonProperty("translator_team_ids")]
    [Display("Translator team IDs to be assigned")]
    [DataSource(typeof(TeamDataHandler))]
    public IEnumerable<string>? TranslatorTeamIds { get; set; }

    [JsonProperty("reviewer_ids")]
    [Display("Reviewer IDs to be assigned")]
    [DataSource(typeof(UserDataHandler))]
    public IEnumerable<string>? ReviwerUserIds { get; set; }

    [JsonProperty("reviewer_team_ids")]
    [Display("Translator team IDs to be assigned")]
    [DataSource(typeof(TeamDataHandler))]
    public IEnumerable<string>? ReviwerTeamIds { get; set; }
}
