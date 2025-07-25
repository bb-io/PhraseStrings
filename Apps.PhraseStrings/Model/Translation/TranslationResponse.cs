using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Translation
{
    public class ListTranslationsResponse
    {
        [Display("Translations")]
        public List<TranslationResponse>? Translations { get; set; }
    }

    public class TranslationResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("content")]
        public string? Content { get; set; } = string.Empty;

        [JsonProperty("unverified")]
        public bool? Unverified { get; set; }

        [JsonProperty("excluded")]
        public bool? Excluded { get; set; }

        [JsonProperty("plural_suffix")]
        [Display("Plural suffix")]
        public string? PluralSuffix { get; set; }

        [JsonProperty("key")]
        public TranslationKey? Key { get; set; }

        [JsonProperty("locale")]
        public TranslationLocale? Locale { get; set; }

        [JsonProperty("placeholders")]
        [Display("Place holders")]
        public List<string>? Placeholders { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("user")]
        public TranslationUser? User { get; set; }

        [JsonProperty("word_count")]
        [Display("Word count")]
        public int? WordCount { get; set; }
    }

    public class TranslationKey
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("plural")]
        public bool Plural { get; set; }
    }

    public class TranslationLocale
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;
    }

    public class TranslationUser
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
