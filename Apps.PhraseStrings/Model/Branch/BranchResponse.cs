using Apps.PhraseStrings.Model.User;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Branch
{
    public class BranchResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("merged_at")]
        public DateTime? MergedAt { get; set; }

        [JsonProperty("merged_by")]
        public UserInfo MergedBy { get; set; } = new();

        [JsonProperty("created_by")]
        public UserInfo CreatedBy { get; set; } = new();

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;
    }
}
