using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Team;

public class GetTeamByNameRequest
{
    [Display("Account ID")]
    [DataSource(typeof(AccountDataHandler))]
    public string AccountId { get; set; } = string.Empty;

    [Display("Team name")]
    public string TeamName { get; set; } = string.Empty;

    [Display("Check is name contains (default exact match)")]
    public bool? UseContains { get; set; } = false;
}
