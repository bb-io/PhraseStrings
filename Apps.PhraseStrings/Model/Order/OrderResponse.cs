using Apps.PhraseStrings.Model.Job;
using Blackbird.Applications.Sdk.Common;
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
        [Display("Amount in cents")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("translation_type")]
        [Display("Translation type")]
        public string? TranslationType { get; set; }

        [JsonProperty("progress_percent")]
        [Display("Progress percent")]
        public int? ProgressPercent { get; set; }

        [JsonProperty("source_locale")]
        [Display("Source locale")]
        public LocaleInfo? SourceLocale { get; set; }

        [JsonProperty("target_locales")]
        [Display("Target locales")]
        public List<LocaleInfo>? TargetLocales { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("styleguide")]
        public StyleguideInfo Styleguide { get; set; }

        [JsonProperty("unverify_translations_upon_delivery")]
        [Display("Unverify translations upon delivery")]
        public bool? UnverifyTranslationsUponDelivery { get; set; }

        [JsonProperty("quality")]
        public bool? Quality { get; set; }

        [JsonProperty("priority")]
        public bool? Priority { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
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
