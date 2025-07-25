using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Order
{
    public class CreateOrderRequest
    {
        [JsonProperty("branch")]
        public string? Branch { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("lsp")]
        public string Lsp { get; set; } = string.Empty;

        [JsonProperty("source_locale_id")]
        public string? SourceLocaleId { get; set; }

        [JsonProperty("target_locale_ids")]
        public List<string>? TargetLocaleIds { get; set; }

        [JsonProperty("translation_type")]
        public string? TranslationType { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("styleguide_id")]
        public string? StyleguideId { get; set; }

        [JsonProperty("unverify_translations_upon_delivery")]
        public bool? UnverifyTranslationsUponDelivery { get; set; }

        [JsonProperty("include_untranslated_keys")]
        public bool? IncludeUntranslatedKeys { get; set; }

        [JsonProperty("include_unverified_translations")]
        public bool? IncludeUnverifiedTranslations { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; } = string.Empty;

        [JsonProperty("quality")]
        public bool? Quality { get; set; }

        [JsonProperty("priority")]
        public bool? Priority { get; set; }
    }
}
