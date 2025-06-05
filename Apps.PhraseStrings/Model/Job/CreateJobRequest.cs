using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class CreateJobRequest
    {
        public string Name { get; set; }

        [Display("Branch")]
        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("source_locale_id")]
        [Display("Source locale ID")]
        public string? SourceLocaleId { get; set; }

        [Display("Briefing")]
        [JsonProperty("briefing")]
        public string? Briefing { get; set; }

        [Display("Due date")]
        [JsonProperty("due_date")]
        public DateTime? DueDate { get; set; }

        [Display("Ticket URL")]
        [JsonProperty("ticket_url")]
        public string? TicketUrl { get; set; }

        [Display("Tags")]
        [JsonProperty("tags")]
        public IEnumerable<string>? Tags { get; set; }

        [Display("Translation key IDs")]
        [JsonProperty("translation_key_ids")]
        public IEnumerable<string>? TranslationKeyIds { get; set; }

        [Display("Job template ID")]
        [JsonProperty("job_template_id")]
        public IEnumerable<string>? JobTemplateId { get; set; }
    }
}
