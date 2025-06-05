using Apps.PhraseStrings.Model.Branch;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class ListKeysResponse
    {
        public List<KeyResponse>? Keys { get; set; }
    }

    public class KeyResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("name_hash")]
        public string? NameHash { get; set; }

        [JsonProperty("plural")]
        public bool? Plural { get; set; }

        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }

        [JsonProperty("data_type")]
        public string? DataType { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("name_plural")]
        public string? NamePlural { get; set; }

        [JsonProperty("comments_count")]
        public int? CommentsCount { get; set; }

        [JsonProperty("max_characters_allowed")]
        public int? MaxCharactersAllowed { get; set; }

        [JsonProperty("screenshot_url")]
        public string? ScreenshotUrl { get; set; }

        [JsonProperty("unformatted")]
        public bool? Unformatted { get; set; }

        [JsonProperty("xml_space_preserve")]
        public bool? XmlSpacePreserve { get; set; }

        [JsonProperty("original_file")]
        public string? OriginalFile { get; set; }

        [JsonProperty("format_value_type")]
        public string? FormatValueType { get; set; }

        [JsonProperty("creator")]
        public UserInfo? Creator { get; set; }

        [JsonProperty("custom_metadata")]
        public Dictionary<string, string>? CustomMetadata { get; set; }
    }
}
