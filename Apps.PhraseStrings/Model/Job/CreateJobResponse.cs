using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class CreateJobResponse
    {
        [JsonProperty("id")]
        [Display("Job ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("briefing")]
        public string? Briefing { get; set; }

        [JsonProperty("due_date")]
        [Display("Due date")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("ticket_url")]
        [Display("Ticket URL")]
        public string? TicketUrl { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")    ]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("project")]
        public ProjectInfo? Project { get; set; }

        [JsonProperty("owner")]
        public OwnerInfo? Owner { get; set; }

        [JsonProperty("job_tag_name")]
        [Display("Job tag name")]
        public string? JobTagName { get; set; }

        [JsonProperty("source_translations_updated_at")]
        [Display("Source translations updated at")]
        public DateTime? SourceTranslationsUpdatedAt { get; set; }

        [JsonProperty("source_locale")]
        [Display("Source locale")]
        public LocaleInfo? SourceLocale { get; set; }

        [JsonProperty("locales")]
        public List<LocaleInfo>? Locales { get; set; }

        [JsonProperty("keys")]
        public List<KeyInfo>? Keys { get; set; }
    }

    public class OwnerInfo
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("username")]
        [Display("Username")]
        public string? Username { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class LocaleInfo
    {
        [Display("Locale ID")]
        [JsonProperty("id")]
        public string? Id { get; set; }

        [Display("Locale name")]
        [JsonProperty("name")]
        public string? Name { get; set; }

        [Display("Locale ISO code")]
        [JsonProperty("code")]
        public string? Code { get; set; }
    }

    public class KeyInfo
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
