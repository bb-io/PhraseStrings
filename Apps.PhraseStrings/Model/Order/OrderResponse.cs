using Apps.PhraseStrings.Model.Job;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Order
{
    public class ListOrdersResponse
    {
        public List<OrderResponse> Orders { get; set; } = new List<OrderResponse>();
    }

    public class OrderResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lsp")]
        public string? Lsp { get; set; }

        [JsonProperty("amount_in_cents")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("translation_type")]
        public string? TranslationType { get; set; }

        [JsonProperty("progress_percent")]
        public int? ProgressPercent { get; set; }

        [JsonProperty("source_locale")]
        public LocaleInfo? SourceLocale { get; set; }

        [JsonProperty("target_locales")]
        public List<LocaleInfo>? TargetLocales { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("styleguide")]
        public StyleguideInfo Styleguide { get; set; }

        [JsonProperty("unverify_translations_upon_delivery")]
        public bool? UnverifyTranslationsUponDelivery { get; set; }

        [JsonProperty("quality")]
        public bool? Quality { get; set; }

        [JsonProperty("priority")]
        public bool? Priority { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class StyleguideInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

}
