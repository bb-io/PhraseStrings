using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Handler;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;

namespace Apps.PhraseStrings.Webhooks
{
    [WebhookList]
    public class WebhookList
    {
        [Webhook("On job completed", typeof(JobCompletedHandler), Description = "Triggers when job completed")]
        public Task<WebhookResponse<JobCompleteWebhookResponse>> OnJobCompleted(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            var root = GetPayload<JobCompleteWebhookResponse>(webhookRequest);

            return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>()
            {
                Result = root
            });
        }


        private WebhookResponse<T> GetPreflightResponse<T>() where T : class => new()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            ReceivedWebhookRequestType = WebhookRequestType.Preflight
        };

        private T GetPayload<T>(WebhookRequest webhookRequest) where T : class
        {
            var payload = webhookRequest.Body.ToString();
            ArgumentException.ThrowIfNullOrEmpty(payload, nameof(webhookRequest.Body));

            return JsonConvert.DeserializeObject<T>(payload)!;
        }
    }
}
