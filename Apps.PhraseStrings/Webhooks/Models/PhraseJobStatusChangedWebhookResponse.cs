using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models;

public class PhraseJobStatusChangedWebhookResponse
{
    [JsonProperty("event")]
    public string? Event { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("sent_at")]
    [Display("Sent at")]
    public DateTime? SentAt { get; set; }

    [JsonProperty("user")]
    public WebhookUser? User { get; set; }

    [JsonProperty("project")]
    public WebhookProject? Project { get; set; }

    [JsonProperty("branch")]
    public WebhookBranch? Branch { get; set; }

    [JsonProperty("job")]
    public WebhookJob? Job { get; set; }
}
