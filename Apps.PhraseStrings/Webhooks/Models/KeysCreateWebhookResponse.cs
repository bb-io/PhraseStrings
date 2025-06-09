using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class KeysCreateWebhookResponse
    {
        [JsonProperty("event")]
        public string? Event { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("user")]
        public WebhookUser? User { get; set; }

        [JsonProperty("project")]
        public WebhookProject? Project { get; set; }

        [JsonProperty("branch")]
        public WebhookBranch? Branch { get; set; }

        [JsonProperty("key")]
        public WebhookKey? Key { get; set; }
    }

    public class WebhookKey
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("name_hash")]
        [Display("Name hash")]
        public string? NameHash { get; set; }

        [JsonProperty("plural")]
        public bool? Plural { get; set; }

        [JsonProperty("max_characters_allowed")]
        [Display("Max characters allowed")]
        public int? MaxCharactersAllowed { get; set; }

        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
