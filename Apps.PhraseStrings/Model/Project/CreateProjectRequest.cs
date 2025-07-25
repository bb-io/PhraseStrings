using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Project
{
    public class CreateProjectRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("main_format")]
        [Display("Main format")]
        public string? MainFormat { get; set; }

        [JsonProperty("media")]
        public string? Media { get; set; }

        [JsonProperty("shares_translation_memory")]
        [Display("Shares translation memory")]
        public bool? SharesTranslationMemory { get; set; }

        [DefinitionIgnore]
        [JsonProperty("project_image")]
        [Display("Project image")]
        public byte[]? ProjectImage { get; set; }

        [JsonProperty("remove_project_image")]
        [Display("Remove project image")]
        public bool? RemoveProjectImage { get; set; }

        [JsonProperty("account_id")]
        [Display("Account ID")]
        public string? AccountId { get; set; }

        [JsonProperty("point_of_contact")]
        [Display("Point of contact")]
        public string? PointOfContact { get; set; }

        [JsonProperty("source_project_id")]
        [Display("Source project ID")]
        public string? SourceProjectId { get; set; }

        [JsonProperty("workflow")]
        [Display("Workflow")]
        public string? Workflow { get; set; }

        [JsonProperty("machine_translation_enabled")]
        [Display("Machine translation enabled")]
        public bool? MachineTranslationEnabled { get; set; }

        [JsonProperty("enable_branching")]
        [Display("Enable branching")]
        public bool? EnableBranching { get; set; }

        [JsonProperty("protect_master_branch")]
        [Display("Protect master branch")]
        public bool? ProtectMasterBranch { get; set; }

        [JsonProperty("enable_all_data_type_translation_keys_for_translators")]
        [Display("Enable all data type translation keys for translators")]
        public bool? EnableAllDataTypeTranslationKeysForTranslators { get; set; }

        [JsonProperty("enable_icu_message_format")]
        [Display("Enable ICU message format")]
        public bool? EnableIcuMessageFormat { get; set; }

        [JsonProperty("zero_plural_form_enabled")]
        [Display("Zero plural form enabled")]
        public bool? ZeroPluralFormEnabled { get; set; }

        [JsonProperty("autotranslate_enabled")]
        [Display("Autotranslate enabled")]
        public bool? AutotranslateEnabled { get; set; }

        [JsonProperty("autotranslate_check_new_translation_keys")]
        [Display("Autotranslate check new translation keys")]
        public bool? AutotranslateCheckNewTranslationKeys { get; set; }

        [JsonProperty("autotranslate_check_new_uploads")]
        [Display("Autotranslate check new uploads")]
        public bool? AutotranslateCheckNewUploads { get; set; }

        [JsonProperty("autotranslate_check_new_locales")]
        [Display("Autotranslate check new locales")]
        public bool? AutotranslateCheckNewLocales { get; set; }

        [JsonProperty("autotranslate_mark_as_unverified")]
        [Display("Autotranslate mark as unverified")]
        public bool? AutotranslateMarkAsUnverified { get; set; }

        [JsonProperty("autotranslate_use_machine_translation")]
        [Display("Autotranslate use machine translation")]
        public bool? AutotranslateUseMachineTranslation { get; set; }

        [JsonProperty("autotranslate_use_translation_memory")]
        [Display("Autotranslate use translation memory")]
        public bool? AutotranslateUseTranslationMemory { get; set; }
    }
}
