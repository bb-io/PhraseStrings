using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Translation
{
    public class CreateTranslationRequest
    {
        [JsonProperty("branch")]
        public string? Branch { get; set; }

        [JsonProperty("locale_id")]
        [Display("Locale ID")]
        [DataSource(typeof(LocaleDataHandler))]
        public string? LocaleId { get; set; }

        [JsonProperty("key_id")]
        [Display("Key ID")]
        [DataSource(typeof(KeyDataHandler))]
        public string? KeyId { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("plural_suffix")]
        [Display("Plural Suffix")]
        public string? PluralSuffix { get; set; }

        [JsonProperty("unverified")]
        public bool? Unverified { get; set; }

        [JsonProperty("excluded")]
        public bool? Excluded { get; set; }

        [JsonProperty("autotranslate")]
        public bool? Autotranslate { get; set; }
    }
}
