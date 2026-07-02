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
        [Display("Translation ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("content")]
        [Display("Content")]
        public string? Content { get; set; } = string.Empty;

        [JsonProperty("unverified")]
        [Display("Unverified")]
        public bool? Unverified { get; set; }

        [JsonProperty("excluded")]
        [Display("Excluded")]
        public bool? Excluded { get; set; }

        [JsonProperty("plural_suffix")]
        [Display("Plural suffix")]
        public string? PluralSuffix { get; set; }

        [JsonProperty("key")]
        [Display("Key")]
        public TranslationKey? Key { get; set; }

        [JsonProperty("locale")]
        [Display("Locale")]
        public TranslationLocale? Locale { get; set; }

        [JsonProperty("placeholders")]
        [Display("Placeholders")]
        public List<string>? Placeholders { get; set; }

        [JsonProperty("state")]
        [Display("State")]
        public string? State { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("user")]
        [Display("User")]
        public TranslationUser? User { get; set; }

        [JsonProperty("word_count")]
        [Display("Word count")]
        public int? WordCount { get; set; }
    }

    public class TranslationKey
    {
        [JsonProperty("id")]
        [Display("Key ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("Key name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("plural")]
        [Display("Plural")]
        public bool Plural { get; set; }
    }

    public class TranslationLocale
    {
        [JsonProperty("id")]
        [Display("Locale ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("Locale name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("code")]
        [Display("Locale code")]
        public string Code { get; set; } = string.Empty;
    }

    public class TranslationUser
    {
        [JsonProperty("id")]
        [Display("User ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("username")]
        [Display("Username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("User name")]
        public string Name { get; set; } = string.Empty;
    }
}
