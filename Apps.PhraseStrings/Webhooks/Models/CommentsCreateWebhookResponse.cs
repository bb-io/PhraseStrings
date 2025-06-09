using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class CommentsCreateWebhookResponse
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

        [JsonProperty("comment")]
        public WebhookComment? Comment { get; set; }
    }

    public class WebhookComment
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("has_replies")]
        [Display("Has replies")]
        public bool? HasReplies { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("user")]
        public WebhookUser? User { get; set; }

        [JsonProperty("mentioned_users")]
        [Display("Mentioned users")]
        public List<WebhookUser>? MentionedUsers { get; set; }

        [JsonProperty("locales")]
        public List<WebhookLocale>? Locales { get; set; }

        [JsonProperty("key")]
        public WebhookKey? Key { get; set; }
    }

    public class WebhookLocale
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }
    }
}
