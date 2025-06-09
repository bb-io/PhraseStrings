using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Locale
{
    public class DownloadLocaleRequest
    {
        [Display("Branch")]
        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [Display("File format")]
        [JsonProperty("file_format")]
        [DataSource(typeof(FormatDataHandler))]
        public string FileFormat { get; set; }

        [Display("Tags")]
        [JsonProperty("tags")]
        public string? Tags { get; set; }

        [Display("Include empty  translations")]
        [JsonProperty("include_empty_translations")]
        public bool? IncludeEmptyTranslations { get; set; }

        [Display("Exclude empty zero forms")]
        [JsonProperty("exclude_empty_zero_forms")]
        public bool? ExcludeEmptyZeroForms { get; set; }

        [Display("Include translated keys")]
        [JsonProperty("include_translated_keys")]
        public bool? IncludeTranslatedKeys { get; set; }
        //
        [Display("Keep nontranslate tags")]
        [JsonProperty("keep_notranslate_tags")]
        public bool? KeepNotranslateTags { get; set; }

        [Display("Encoding")]
        [JsonProperty("encoding")]
        public string? Encoding { get; set; }

        [Display("Include unverified translations")]
        [JsonProperty("include_unverified_translations")]
        public bool? IncludeUnverifiedTranslations { get; set; }

        [Display("Use last reviewed version")]
        [JsonProperty("use_last_reviewed_version")]
        public bool? UseLastReviewedVersion { get; set; }

        [Display("Fallback locale ID")]
        [JsonProperty("fallback_locale_id")]
        public string? FallbackLocaleId { get; set; }

        [Display("Source locale ID")]
        [JsonProperty("source_locale_id")]
        public string? SourceLocaleId { get; set; }

        [Display("Translation key prefix")]
        [JsonProperty("translation_key_prefix")]
        public string? TranslationKeyPrefix { get; set; }

        [Display("Filter by prefix")]
        [JsonProperty("filter_by_prefix")]
        public bool? FilterByPrefix { get; set; }

        [Display("Locale IDs")]
        [JsonProperty("locale_ids")]
        [DataSource(typeof(LocaleDataHandler))]
        public List<string>? LocaleIds { get; set; }
    }
}
