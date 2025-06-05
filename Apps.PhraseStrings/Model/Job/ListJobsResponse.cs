using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class ListJobsResponse
    {
        public List<JobResponse>? Jobs { get; set; }
    }

    public class JobResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("briefing")]
        public string? Briefing { get; set; }

        [JsonProperty("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("ticket_url")]
        public string? TicketUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("project")]
        public ProjectInfo? Project { get; set; }
    }

    public class ProjectInfo
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("main_format")]
        public string? MainFormat { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
