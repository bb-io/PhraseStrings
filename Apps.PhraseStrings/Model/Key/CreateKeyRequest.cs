using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class CreateKeyRequest
    {
        [Display("Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Display("Branch")]
        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [Display("Description")]
        [JsonProperty("description")]
        public string? Description { get; set; }

        [Display("Plural")]
        [JsonProperty("plural")]
        public bool? Plural { get; set; }

        [Display("Plural name")]
        [JsonProperty("name_plural")]
        public string? NamePlural { get; set; }

        [Display("Data type")]
        [JsonProperty("data_type")]
        public string? DataType { get; set; }

        [Display("Tags (comma-separated)")]
        [JsonProperty("tags")]
        public string? Tags { get; set; }

        [Display("Max characters allowed")]
        [JsonProperty("max_characters_allowed")]
        public int? MaxCharactersAllowed { get; set; }

        [Display("Unformatted")]
        [JsonProperty("unformatted")]
        public bool? Unformatted { get; set; }

        [Display("Default translation content")]
        [JsonProperty("default_translation_content")]
        public string? DefaultTranslationContent { get; set; }

        [Display("Autotranslate")]
        [JsonProperty("autotranslate")]
        public bool? Autotranslate { get; set; }

        [Display("XML space preserve")]
        [JsonProperty("xml_space_preserve")]
        public bool? XmlSpacePreserve { get; set; }

        [Display("Original file")]
        [JsonProperty("original_file")]
        public string? OriginalFile { get; set; }

        [Display("Localized format string")]
        [JsonProperty("localized_format_string")]
        public string? LocalizedFormatString { get; set; }

        [Display("Localized format key")]
        [JsonProperty("localized_format_key")]
        public string? LocalizedFormatKey { get; set; }
    }
}
