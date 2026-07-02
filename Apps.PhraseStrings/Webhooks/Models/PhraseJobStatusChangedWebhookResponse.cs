using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models;

public class PhraseJobStatusChangedWebhookResponse
{
    [JsonProperty("event")]
    [Display("Event")]
    public string? Event { get; set; }

    [JsonProperty("message")]
    [Display("Message")]
    public string? Message { get; set; }

    [JsonProperty("sent_at")]
    [Display("Sent at")]
    public DateTime? SentAt { get; set; }

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
