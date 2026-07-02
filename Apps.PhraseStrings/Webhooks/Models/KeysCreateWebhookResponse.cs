using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class KeysCreateWebhookResponse
    {
        [JsonProperty("event")]
        [Display("Event")]
        public string? Event { get; set; }

        [JsonProperty("message")]
        [Display("Message")]
        public string? Message { get; set; }

        [JsonProperty("user")]
        [Display("User")]
        public WebhookUser? User { get; set; }

        [JsonProperty("project")]
        [Display("Project")]
        public WebhookProject? Project { get; set; }

        [JsonProperty("branch")]
        [Display("Branch")]
        public WebhookBranch? Branch { get; set; }

        [JsonProperty("key")]
        [Display("Key")]
        public WebhookKey? Key { get; set; }
    }

    public class WebhookKey
    {
        [JsonProperty("id")]
        [Display("Key ID")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        [Display("Key name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        [Display("Description")]
        public string? Description { get; set; }

        [JsonProperty("name_hash")]
        [Display("Name hash")]
        public string? NameHash { get; set; }

        [JsonProperty("plural")]
        [Display("Plural")]
        public bool? Plural { get; set; }

        [JsonProperty("max_characters_allowed")]
        [Display("Max characters allowed")]
        public int? MaxCharactersAllowed { get; set; }

        [JsonProperty("tags")]
        [Display("Tags")]
        public List<string>? Tags { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
