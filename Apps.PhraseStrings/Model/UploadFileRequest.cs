using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model
{
    public class UploadFileRequest
    {
        public FileReference File { get; set; }

        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("file_format")]
        [DataSource(typeof(FormatDataHandler))]
        public string FileFormat { get; set; } = string.Empty;

        [JsonProperty("locale_id")]
        public string LocaleId { get; set; } = string.Empty;

        [JsonProperty("tags")]
        public string? Tags { get; set; }

        [JsonProperty("update_translations")]
        public bool? UpdateTranslations { get; set; }

        [JsonProperty("update_translation_keys")]
        public bool? UpdateTranslationKeys { get; set; }

        [JsonProperty("update_translations_on_source_match")]
        public bool? UpdateTranslationsOnSourceMatch { get; set; }

        [JsonProperty("update_descriptions")]
        public bool? UpdateDescriptions { get; set; }

        [JsonProperty("skip_upload_tags")]
        public bool? SkipUploadTags { get; set; }

        [JsonProperty("skip_unverification")]
        public bool? SkipUnverification { get; set; }

        [JsonProperty("file_encoding")]
        public string? FileEncoding { get; set; }

        [JsonProperty("autotranslate")]
        public bool? Autotranslate { get; set; }

        [JsonProperty("verify_mentioned_translations")]
        public bool? VerifyMentionedTranslations { get; set; }

        [JsonProperty("mark_reviewed")]
        public bool? MarkReviewed { get; set; }

        [JsonProperty("tag_only_affected_keys")]
        public bool? TagOnlyAffectedKeys { get; set; }

        [JsonProperty("translation_key_prefix")]
        public string? TranslationKeyPrefix { get; set; }
    }
}
