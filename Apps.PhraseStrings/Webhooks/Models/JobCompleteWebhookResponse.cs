using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class JobCompleteWebhookResponse
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

        [JsonProperty("job")]
        [Display("Job")]
        public WebhookJob? Job { get; set; }
    }

    public class WebhookUser
    {
        [JsonProperty("id")]
        [Display("User ID")]
        public string? Id { get; set; }

        [JsonProperty("username")]
        [Display("Username")]
        public string? Username { get; set; }

        [JsonProperty("name")]
        [Display("User name")]
        public string? Name { get; set; }

        [JsonProperty("gravatar_uid")]
        [Display("Gravatar UID")]
        public string? GravatarUid { get; set; }
    }

    public class WebhookProject
    {
        [JsonProperty("id")]
        [Display("Project ID")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        [Display("Project name")]
        public string? Name { get; set; }

        [JsonProperty("slug")]
        [Display("Project slug")]
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
        public WebhookUser? PointOfContact { get; set; }
    }

    public class WebhookBranch
    {
        [JsonProperty("name")]
        [Display("Branch name")]
        public string? Name { get; set; }
    }

    public class WebhookJob
    {
        [JsonProperty("id")]
        [Display("Job ID")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        [Display("Job name")]
        public string? Name { get; set; }

        [JsonProperty("briefing")]
        [Display("Briefing")]
        public string? Briefing { get; set; }

        [JsonProperty("due_date")]
        [Display("Due date")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("review_due_date")]
        [Display("Review due date")]
        public DateTime? ReviewDueDate { get; set; }

        [JsonProperty("state")]
        [Display("State")]
        public string? State { get; set; }

        [JsonProperty("owner")]
        [Display("Owner")]
        public WebhookUser? Owner { get; set; }

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
