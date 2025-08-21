using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job;

public class UpdateJobRequest
{
    [Display("Branch")]
    [JsonProperty("branch")]
    [DataSource(typeof(BranchDataHandler))]
    public string? Branch { get; set; }

    [JsonProperty("name")]
    [Display("Job name")]
    public string? Name { get; set; }

    [Display("Briefing")]
    [JsonProperty("briefing")]
    public string? Briefing { get; set; }

    [Display("Due date")]
    [JsonProperty("due_date")]
    public DateTime? DueDate { get; set; }

    [Display("Ticket URL")]
    [JsonProperty("ticket_url")]
    public string? TicketUrl { get; set; }
}
