using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model
{
    public class UploadFileRequest
    {
        [Display("File")]
        public FileReference File { get; set; } = new();

        [JsonProperty("branch")]
        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("file_format")]
        [Display("File format")]
        [DataSource(typeof(FormatDataHandler))]
        public string FileFormat { get; set; } = string.Empty;

        [JsonProperty("locale_id")]
        [Display("Locale ID")]
        public string LocaleId { get; set; } = string.Empty;

        [JsonProperty("tags")]
        [Display("Tags")]
        public string? Tags { get; set; }

        [JsonProperty("update_translations")]
        [Display("Update translations")]
        public bool? UpdateTranslations { get; set; }

        [JsonProperty("update_translation_keys")]
        [Display("Update translation keys")]
        public bool? UpdateTranslationKeys { get; set; }

        [JsonProperty("update_translations_on_source_match")]
        [Display("Update translations on source match")]
        public bool? UpdateTranslationsOnSourceMatch { get; set; }

        [JsonProperty("update_descriptions")]
        [Display("Update descriptions")]
        public bool? UpdateDescriptions { get; set; }

        [JsonProperty("skip_upload_tags")]
        [Display("Skip upload tags")]
        public bool? SkipUploadTags { get; set; }

        [JsonProperty("skip_unverification")]
        [Display("Skip unverification")]
        public bool? SkipUnverification { get; set; }

        [JsonProperty("file_encoding")]
        [Display("File encoding")]
        public string? FileEncoding { get; set; }

        [JsonProperty("autotranslate")]
        [Display("Autotranslate")]
        public bool? Autotranslate { get; set; }

        [JsonProperty("verify_mentioned_translations")]
        [Display("Verify mentioned translations")]
        public bool? VerifyMentionedTranslations { get; set; }

        [JsonProperty("mark_reviewed")]
        [Display("Mark reviewed")]
        public bool? MarkReviewed { get; set; }

        [JsonProperty("tag_only_affected_keys")]
        [Display("Tag only affected keys")]
        public bool? TagOnlyAffectedKeys { get; set; }

        [JsonProperty("translation_key_prefix")]
        [Display("Translation key prefix")]
        public string? TranslationKeyPrefix { get; set; }
    }
}
