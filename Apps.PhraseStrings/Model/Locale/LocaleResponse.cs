using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Locale
{
    public class LocaleResponse
    {
        [JsonProperty("id")]
        [Display("Locale ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("Locale name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("code")]
        [Display("Locale ISO code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("default")]
        [Display("Is locale default?")]
        public bool IsDefault { get; set; }

        [JsonProperty("main")]
        [Display("Is locale main?")]
        public bool IsMain { get; set; }

        [JsonProperty("rtl")]
        [Display("Is locale right-to-Left?")]
        public bool IsRtl { get; set; }

        [JsonProperty("plural_forms")]
        [Display("Plural forms")]
        public List<string> PluralForms { get; set; } = [];

        [JsonProperty("source_locale")]
        [Display("Source locale")]
        public LocaleReference SourceLocale { get; set; } = new();

        [JsonProperty("fallback_locale")]
        [Display("Fallback locale")]
        public LocaleReference FallbackLocale { get; set; } = new();

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class LocaleReference
    {
        [JsonProperty("id")]
        [Display("Locale ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("Locale name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("code")]
        [Display("Locale ISO code")]
        public string Code { get; set; } = string.Empty;
    }
}
