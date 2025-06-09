using Apps.PhraseStrings.Model.Branch;
using Blackbird.Applications.Sdk.Common;
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
        [Display("Hash name")]
        public string? NameHash { get; set; }

        [JsonProperty("plural")]
        public bool? Plural { get; set; }

        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }

        [JsonProperty("data_type")]
        [Display("Data type")]
        public string? DataType { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("name_plural")]
        [Display("Name hash")]
        public string? NamePlural { get; set; }

        [JsonProperty("comments_count")]
        [Display("Comments count")]
        public int? CommentsCount { get; set; }

        [JsonProperty("max_characters_allowed")]
        [Display("Max characters allowed")]
        public int? MaxCharactersAllowed { get; set; }

        [JsonProperty("screenshot_url")]
        [Display("Screenshot URL")]
        public string? ScreenshotUrl { get; set; }

        [JsonProperty("unformatted")]
        public bool? Unformatted { get; set; }

        [JsonProperty("xml_space_preserve")]
        [Display("XML space preserve")]
        public bool? XmlSpacePreserve { get; set; }

        [JsonProperty("original_file")]
        [Display("Original file")]
        public string? OriginalFile { get; set; }

        [JsonProperty("format_value_type")]
        [Display("Format value type")]
        public string? FormatValueType { get; set; }

        [JsonProperty("creator")]
        public UserInfo? Creator { get; set; }

        [JsonProperty("custom_metadata")]
        [Display("Custom metadata")]
        public Dictionary<string, string>? CustomMetadata { get; set; }
    }
}
