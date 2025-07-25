using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model
{
    public class ImportResponse
    {
        [Display("File ID")]
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("filename")]
        public string? Filename { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("summary")]
        public ImportSummary? Summary { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class ImportSummary
    {
        [JsonProperty("locales_created")]
        public int? LocalesCreated { get; set; }

        [JsonProperty("translation_keys_created")]
        public int? TranslationKeysCreated { get; set; }

        [JsonProperty("translation_keys_updated")]
        public int? TranslationKeysUpdated { get; set; }

        [JsonProperty("translation_keys_unmentioned")]
        public int? TranslationKeysUnmentioned { get; set; }

        [JsonProperty("translations_created")]
        public int? TranslationsCreated { get; set; }

        [JsonProperty("translations_updated")]
        public int? TranslationsUpdated { get; set; }

        [JsonProperty("tags_created")]
        public int? TagsCreated { get; set; }

        [JsonProperty("translation_keys_ignored")]
        public int? TranslationKeysIgnored { get; set; }
    }
}
