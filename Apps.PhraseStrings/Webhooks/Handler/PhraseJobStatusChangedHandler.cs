using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.PhraseStrings.Webhooks.Handler;

public class PhraseJobStatusChangedHandler(
    InvocationContext invocationContext,
    [WebhookParameter(true)] PhraseJobStatusChangeRequest input)
    : PhraseStringsInvocable(invocationContext), IWebhookEventHandler
{
    public async Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var events = GetSelectedEvents().ToList();
        if (events.Count == 0)
            throw new PluginMisconfigurationException("Select at least one event to react to.");

        var projectIds = GetProjectIds().ToList();
        if (projectIds.Count == 0)
            throw new PluginMisconfigurationException("Select at least one project ID.");

        foreach (var projectId in projectIds)
        {
            var request = new RestRequest($"/v2/projects/{projectId}/webhooks", Method.Post)
                .AddHeader("accept", "application/json")
                .AddJsonBody(new
                {
                    events = string.Join(",", events),
                    callback_url = values["payloadUrl"]
                });

            await Client.ExecuteWithErrorHandling(request);
        }
    }

    public async Task UnsubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        foreach (var projectId in GetProjectIds())
        {
            var listRequest = new RestRequest($"/v2/projects/{projectId}/webhooks", Method.Get)
                .AddHeader("accept", "application/json");

            var webhooks = await Client.ExecuteWithErrorHandling<List<WebhookResponse>>(listRequest);
            var matchingWebhooks = webhooks.Where(webhook => webhook.CallbackUrl == values["payloadUrl"]);

            foreach (var webhook in matchingWebhooks)
            {
                if (string.IsNullOrWhiteSpace(webhook.Id))
                    continue;

                var deleteRequest = new RestRequest($"/v2/projects/{projectId}/webhooks/{webhook.Id}", Method.Delete)
                    .AddHeader("accept", "application/json");

                await Client.ExecuteWithErrorHandling(deleteRequest);
            }
        }
    }

    private IEnumerable<string> GetSelectedEvents()
        => input.EventsToReactTo.Where(value => !string.IsNullOrWhiteSpace(value)).Distinct();

    private IEnumerable<string> GetProjectIds()
        => input.ProjectIds.Where(value => !string.IsNullOrWhiteSpace(value)).Distinct();
}
