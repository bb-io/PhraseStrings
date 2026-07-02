using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Order
{
    public class CreateOrderResponse
    {
        [JsonProperty("branch")]
        [Display("Branch")]
        public string? Branch { get; set; }

        [JsonProperty("name")]
        [Display("Order name")]
        public string? Name { get; set; }

        [JsonProperty("lsp")]
        [Display("LSP")]
        public string? Lsp { get; set; }

        [JsonProperty("source_locale_id")]
        [Display("Source locale ID")]
        public string? SourceLocaleId { get; set; }

        [JsonProperty("target_locale_ids")]
        [Display("Target locale IDs")]
        public List<string>? TargetLocaleIds { get; set; }

        [JsonProperty("translation_type")]
        [Display("Translation type")]
        public string? TranslationType { get; set; }

        [JsonProperty("tag")]
        [Display("Tag")]
        public string? Tag { get; set; }

        [JsonProperty("message")]
        [Display("Message")]
        public string? Message { get; set; }

        [JsonProperty("styleguide_id")]
        [Display("Styleguide ID")]
        public string? StyleguideId { get; set; }

        [JsonProperty("unverify_translations_upon_delivery")]
        [Display("Unverify translations upon delivery")]
        public bool? UnverifyTranslationsUponDelivery { get; set; }

        [JsonProperty("include_untranslated_keys")]
        [Display("Include untranslated keys")]
        public bool? IncludeUntranslatedKeys { get; set; }

        [JsonProperty("include_unverified_translations")]
        [Display("Include unverified translations")]
        public bool? IncludeUnverifiedTranslations { get; set; }

        [JsonProperty("category")]
        [Display("Category")]
        public string Category { get; set; } = string.Empty;

        [JsonProperty("quality")]
        [Display("Quality")]
        public bool? Quality { get; set; }

        [JsonProperty("priority")]
        [Display("Priority")]
        public bool? Priority { get; set; }
    }
}
