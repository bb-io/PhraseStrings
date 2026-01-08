using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Handler;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;

namespace Apps.PhraseStrings.Webhooks
{
    [WebhookList]
    public class WebhookList(InvocationContext invocationContext) : BaseInvocable(invocationContext)
    {
        [Webhook("On job completed", typeof(JobCompletedHandler), Description = "Triggers when a job is completed")]
        public Task<WebhookResponse<JobCompleteWebhookResponse>> OnJobCompleted(WebhookRequest webhookRequest, 
            [WebhookParameter(true)] ProjectRequest project,
            [WebhookParameter] WebhookJobRequest job)
        {
            InvocationContext.Logger?.LogError("[PhraseStringsJobCompleted] Invocation webhook", []);

            var requestBody = webhookRequest.Body?.ToString();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                InvocationContext.Logger?.LogError("[PhraseStringsJobCompleted] Webhook body is null or empty", []);
                return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>
                {
                    ReceivedWebhookRequestType = WebhookRequestType.Preflight
                });
            }

            InvocationContext.Logger?.LogError($"[PhraseStringsJobCompleted] Webhook body (FULL): {requestBody}", []);

            JobCompleteWebhookResponse root;
            try
            {
                root = GetPayload<JobCompleteWebhookResponse>(webhookRequest);
            }
            catch (Exception ex)
            {
                InvocationContext.Logger?.LogError(
                    $"[PhraseStringsJobCompleted] GetPayload failed. Error: {ex}. Body(FULL): {requestBody}",
                    []);

                return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>
                {
                    ReceivedWebhookRequestType = WebhookRequestType.Preflight
                });
            }

            if (!string.IsNullOrWhiteSpace(project?.ProjectId) && root.Project?.Id != project.ProjectId)
            {
                InvocationContext.Logger?.LogError(
                    $"[PhraseStringsJobCompleted] Project filter mismatch. Expected: {project.ProjectId}, Actual: {root.Project?.Id}. Body(FULL): {requestBody}",
                    []);

                return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>
                {
                    ReceivedWebhookRequestType = WebhookRequestType.Preflight
                });
            }

            if (!string.IsNullOrWhiteSpace(job?.JobId) && root.Job?.Id != job.JobId)
            {
                InvocationContext.Logger?.LogError(
                    $"[PhraseStringsJobCompleted] Job filter mismatch. Expected: {job.JobId}, Actual: {root.Job?.Id}. Body(FULL): {requestBody}",
                    []);

                return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>
                {
                    ReceivedWebhookRequestType = WebhookRequestType.Preflight
                });
            }

            return Task.FromResult(new WebhookResponse<JobCompleteWebhookResponse>
            {
                Result = root,
                ReceivedWebhookRequestType = WebhookRequestType.Default
            });
        }

        [Webhook("On key created", typeof(KeyCreatedHandler), Description = "Triggers when a key is created")]
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


        [Webhook("On key updated", typeof(KeysUpdatedHandler), Description = "Triggers when a key is updated")]
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

        [Webhook("On comment added to a key", typeof(CommentToKeyAddedHandler), Description = "Triggers when a comment is added to a key")]
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
