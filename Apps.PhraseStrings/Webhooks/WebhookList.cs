using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Handler;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Apps.PhraseStrings.Webhooks
{
    [WebhookList]
    public class WebhookList(InvocationContext invocationContext) : BaseInvocable(invocationContext)
    {
        [Webhook("On job completed", typeof(JobCompletedHandler), Description = "Runs when a job is completed.")]
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

        [Webhook("On job status changed", typeof(PhraseJobStatusChangedHandler), Description = "Runs when a job is created, activated, or completed.")]
        public Task<WebhookResponse<PhraseJobStatusChangedWebhookResponse>> OnPhraseJobStatusChange(
            WebhookRequest webhookRequest,
            [WebhookParameter(true)] PhraseJobStatusChangeRequest input)
        {
            if (!TryGetPayload(webhookRequest, nameof(OnPhraseJobStatusChange), out PhraseJobStatusChangedWebhookResponse? root))
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            var selectedEvents = (input.EventsToReactTo?.Where(value => !string.IsNullOrWhiteSpace(value)) ?? [])
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            if (selectedEvents.Count == 0)
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            if (string.IsNullOrWhiteSpace(root.Event) || !selectedEvents.Contains(root.Event))
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            var selectedProjects = input.ProjectIds?.Where(value => !string.IsNullOrWhiteSpace(value)).ToHashSet(StringComparer.Ordinal);
            if (selectedProjects is not { Count: > 0 })
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            if (root.Project?.Id == null || !selectedProjects.Contains(root.Project.Id))
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            var selectedJobs = input.JobIds?.Where(value => !string.IsNullOrWhiteSpace(value)).ToHashSet(StringComparer.Ordinal);
            if (selectedJobs?.Count > 0 && (root.Job?.Id == null || !selectedJobs.Contains(root.Job.Id)))
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            var jobName = root.Job?.Name ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(input.JobNameContains)
                && !jobName.Contains(input.JobNameContains, StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            if (!string.IsNullOrWhiteSpace(input.JobNameDoesntContain)
                && jobName.Contains(input.JobNameDoesntContain, StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(GetPreflightResponse<PhraseJobStatusChangedWebhookResponse>());

            return Task.FromResult(new WebhookResponse<PhraseJobStatusChangedWebhookResponse>
            {
                Result = root,
                ReceivedWebhookRequestType = WebhookRequestType.Default
            });
        }

        [Webhook("On key created", typeof(KeyCreatedHandler), Description = "Runs when a key is created.")]
        public Task<WebhookResponse<KeysCreateWebhookResponse>> OnKeyCreated(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            if (!TryGetPayload(webhookRequest, nameof(OnKeyCreated), out KeysCreateWebhookResponse? root))
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
            {
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());
            }

            return Task.FromResult(new WebhookResponse<KeysCreateWebhookResponse>()
            {
                Result = root
            });
        }


        [Webhook("On key updated", typeof(KeysUpdatedHandler), Description = "Runs when a key is updated.")]
        public Task<WebhookResponse<KeysCreateWebhookResponse>> OnKeyUpdated(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            if (!TryGetPayload(webhookRequest, nameof(OnKeyUpdated), out KeysCreateWebhookResponse? root))
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
            {
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());
            }

            return Task.FromResult(new WebhookResponse<KeysCreateWebhookResponse>()
            {
                Result = root
            });
        }

        [Webhook("On comment added to a key", typeof(CommentToKeyAddedHandler), Description = "Runs when a comment is added to a key.")]
        public Task<WebhookResponse<KeysCreateWebhookResponse>> OnCommentAddedToKey(WebhookRequest webhookRequest, [WebhookParameter(true)] ProjectRequest project)
        {
            if (!TryGetPayload(webhookRequest, nameof(OnCommentAddedToKey), out KeysCreateWebhookResponse? root))
                return Task.FromResult(GetPreflightResponse<KeysCreateWebhookResponse>());

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

        private bool TryGetPayload<T>(
            WebhookRequest webhookRequest,
            string handlerName,
            [NotNullWhen(true)] out T? payload) where T : class
        {
            payload = null;

            var requestBody = webhookRequest.Body?.ToString();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                InvocationContext.Logger?.LogError($"[{handlerName}] Webhook body is null or empty", []);
                return false;
            }

            try
            {
                payload = GetPayload<T>(webhookRequest);
                return payload is not null;
            }
            catch (Exception ex)
            {
                InvocationContext.Logger?.LogError($"[{handlerName}] GetPayload failed. Error: {ex}. Body(FULL): {requestBody}", []);
                return false;
            }
        }

        private T GetPayload<T>(WebhookRequest webhookRequest) where T : class
        {
            var payload = webhookRequest.Body.ToString();
            ArgumentException.ThrowIfNullOrEmpty(payload, nameof(webhookRequest.Body));

            return JsonConvert.DeserializeObject<T>(payload)!;
        }
    }
}
