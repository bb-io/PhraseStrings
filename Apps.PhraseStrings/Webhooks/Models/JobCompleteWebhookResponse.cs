using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class JobCompleteWebhookResponse
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

        [JsonProperty("job")]
        public WebhookJob? Job { get; set; }
    }

    public class WebhookUser
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("gravatar_uid")]
        [Display("Gravatar UID")]
        public string? GravatarUid { get; set; }
    }

    public class WebhookProject
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("slug")]
        public string? Slug { get; set; }

        [JsonProperty("main_format")]
        [Display("Main format")]
        public string? MainFormat { get; set; }

        [JsonProperty("project_image_url")]
        [Display("Project image URL")]
        public string? ProjectImageUrl { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("point_of_contact")]
        [Display("Point of contact")]
        public string? PointOfContact { get; set; }
    }

    public class WebhookBranch
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class WebhookJob
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

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
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
