using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class ListJobsResponse
    {
        [Display("Jobs")]
        public List<JobResponse>? Jobs { get; set; }
    }

    public class JobResponse
    {
        [JsonProperty("id")]
        [Display("Job ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("Job name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("briefing")]
        [Display("Briefing")]
        public string? Briefing { get; set; }

        [JsonProperty("due_date")]
        [Display("Due date")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("state")]
        [Display("State")]
        public string? State { get; set; }

        [JsonProperty("ticket_url")]
        [Display("Ticket URL")]
        public string? TicketUrl { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("project")]
        [Display("Project")]
        public ProjectInfo? Project { get; set; }
    }

    public class ProjectInfo
    {
        [JsonProperty("id")]
        [Display("Project ID")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        [Display("Project name")]
        public string? Name { get; set; }

        [JsonProperty("main_format")]
        [Display("Main format")]
        public string? MainFormat { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
