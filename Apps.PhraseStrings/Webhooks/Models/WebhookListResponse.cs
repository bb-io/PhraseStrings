using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class WebhookResponse
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("callback_url")]
        public string? CallbackUrl { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("events")]
        public string? Events { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("include_branches")]
        public bool? IncludeBranches { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
