using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class SearchJobsRequest
    {
        [JsonProperty("branch")]
        public string? Branch { get; set; }

        [JsonProperty("owned_by")]
        public string? OwnedBy { get; set; }

        [JsonProperty("assigned_to")]
        public string? AssignedTo { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("updated_since")]
        public DateTime? UpdatedSince { get; set; }
    }
}
