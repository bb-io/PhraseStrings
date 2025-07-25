using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.PhraseStrings.Webhooks.Base
{
    public abstract class PhraseStringsWebhookHandler : PhraseStringsInvocable, IWebhookEventHandler
    {
        protected abstract string SubscriptionEvents { get; }

        private readonly InvocationContext _invocationContext;

        private readonly ProjectRequest _input;

        public PhraseStringsWebhookHandler(InvocationContext invocationContext, [WebhookParameter(true)] ProjectRequest input) : base(invocationContext)
        {
            _invocationContext = invocationContext;
            _input = input;
        }

        public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            var requestBody = new
            {
                events = SubscriptionEvents,
                callback_url = values["payloadUrl"]
            };

            var request = new RestRequest($"/v2/projects/{_input.ProjectId}/webhooks", Method.Post)
               .AddHeader("accept", "application/json")
               .AddJsonBody(requestBody);

            await Client.ExecuteWithErrorHandling(request);
        }

        public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            var wrapper = await GetAllWebhooks();
            var payloadUrl = values["payloadUrl"];

            var webhookToDelete = wrapper
                .FirstOrDefault(w => w.CallbackUrl == payloadUrl);

            if (webhookToDelete == null)
                return;

            var request = new RestRequest($"/v2/projects/{_input.ProjectId}/webhooks/{webhookToDelete.Id}", Method.Delete)
                .AddHeader("accept", "application/json");

            await Client.ExecuteWithErrorHandling(request);
        }

        private async Task<List<WebhookResponse>> GetAllWebhooks()
        {
            var request = new RestRequest($"v2/projects/{_input.ProjectId}/webhooks/", Method.Get)
                .AddHeader("accept", "application/json");

            var response = await Client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<List<WebhookResponse>>(response.Content ?? string.Empty) ?? [];
        }
    }
}
