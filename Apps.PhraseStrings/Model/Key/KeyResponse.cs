using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class KeyResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name_hash")]
        public string NameHash { get; set; }

        [JsonProperty("plural")]
        public bool Plural { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
