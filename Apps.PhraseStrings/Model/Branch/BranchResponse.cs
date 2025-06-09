using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Branch
{
    public class BranchResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("merged_at")]
        public DateTime? MergedAt { get; set; }

        [JsonProperty("merged_by")]
        public UserInfo MergedBy { get; set; }

        [JsonProperty("created_by")]
        public UserInfo CreatedBy { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class UserInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
