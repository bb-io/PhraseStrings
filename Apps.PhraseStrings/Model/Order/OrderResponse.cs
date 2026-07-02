using Apps.PhraseStrings.Model.Job;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Order
{
    public class ListOrdersResponse
    {
        [Display("Orders")]
        public List<OrderResponse> Orders { get; set; } = new List<OrderResponse>();
    }

    public class OrderResponse
    {
        [JsonProperty("id")]
        [Display("Order ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("lsp")]
        [Display("LSP")]
        public string? Lsp { get; set; }

        [JsonProperty("amount_in_cents")]
        [Display("Amount in cents")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        [Display("Currency")]
        public string? Currency { get; set; }

        [JsonProperty("message")]
        [Display("Message")]
        public string? Message { get; set; }

        [JsonProperty("state")]
        [Display("State")]
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
        [Display("Tag")]
        public string? Tag { get; set; }

        [JsonProperty("styleguide")]
        [Display("Styleguide")]
        public StyleguideInfo Styleguide { get; set; } = new();

        [JsonProperty("unverify_translations_upon_delivery")]
        [Display("Unverify translations upon delivery")]
        public bool? UnverifyTranslationsUponDelivery { get; set; }

        [JsonProperty("quality")]
        [Display("Quality")]
        public bool? Quality { get; set; }

        [JsonProperty("priority")]
        [Display("Priority")]
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
        [Display("Styleguide ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("title")]
        [Display("Styleguide title")]
        public string Title { get; set; } = string.Empty;
    }

}
