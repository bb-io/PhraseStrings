using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Handler;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;

namespace Apps.PhraseStrings.Webhooks
{
    [WebhookList]
    public class WebhookList(InvocationContext invocationContext) : BaseInvocable(invocationContext)
    {
        [Webhook("On job completed", typeof(JobCompletedHandler), Description = "Triggers when job completed")]
        public Task<WebhookResponse<JobCompleteWebhookResponse>> OnJobCompleted(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            var root = GetPayload<JobCompleteWebhookResponse>(webhookRequest);

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
            {
                return Task.FromResult(GetPreflightResponse<JobCompleteWebhookResponse>());
            }

            return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>()
            {
                Result = root
            });
        }

        [Webhook("On key created", typeof(KeyCreatedHandler), Description = "Triggers when key created")]
        public Task<WebhookResponse<KeysCreateWebhookResponse>> OnKeyCreated(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            var root = GetPayload<KeysCreateWebhookResponse>(webhookRequest);

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
            {
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());
            }

            return Task.FromResult(new WebhookResponse<KeysCreateWebhookResponse>()
            {
                Result = root
            });
        }


        [Webhook("On key updated", typeof(KeysUpdatedHandler), Description = "Triggers when key updated")]
        public Task<WebhookResponse<KeysCreateWebhookResponse>> OnKeyUpdated(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            var root = GetPayload<KeysCreateWebhookResponse>(webhookRequest);

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
            {
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());
            }

            return Task.FromResult(new WebhookResponse<KeysCreateWebhookResponse>()
            {
                Result = root
            });
        }

        [Webhook("On comment added to a key", typeof(CommentToKeyAddedHandler), Description = "Triggers when comment added to a key")]
        public Task<WebhookResponse<KeysCreateWebhookResponse>> OnCommentAddedToKey(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            var root = GetPayload<KeysCreateWebhookResponse>(webhookRequest);

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
            {
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());
            }

            return Task.FromResult(new WebhookResponse<KeysCreateWebhookResponse>()
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
