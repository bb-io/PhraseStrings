using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Translation
{
    public class UpdateTranslationRequest
    {
        [JsonProperty("branch")]
        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("content")]
        [Display("Content")]
        public string? Content { get; set; }

        [JsonProperty("plural_suffix")]
        [Display("Plural suffix")]
        public string? PluralSuffix { get; set; }

        [JsonProperty("unverified")]
        [Display("Unverified")]
        public bool? Unverified { get; set; }

        [JsonProperty("excluded")]
        [Display("Excluded")]
        public bool? Excluded { get; set; }

        [JsonProperty("autotranslate")]
        [Display("Autotranslate")]
        public bool? Autotranslate { get; set; }
    }
}
