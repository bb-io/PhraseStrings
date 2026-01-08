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
        public async Task<WebhookResponse<JobCompleteWebhookResponse>> OnJobCompleted(WebhookRequest webhookRequest, 
            [WebhookParameter(true)] ProjectRequest project,
            [WebhookParameter] WebhookJobRequest job)
        {
            await WebhookSiteLogger.LogIncomingAsync(webhookRequest, "before_parse", new
            {
                projectFilter = project?.ProjectId,
                jobFilter = job?.JobId
            });

            JobCompleteWebhookResponse root;
            try
            {
                root = GetPayload<JobCompleteWebhookResponse>(webhookRequest);
            }
            catch (Exception ex)
            {
                await WebhookSiteLogger.LogIncomingAsync(webhookRequest, "parse_failed", new
                {
                    error = ex.ToString()
                });
                throw;
            }

            await WebhookSiteLogger.LogIncomingAsync(webhookRequest, "after_parse", new
            {
                parsed = root
            });

            if (project.ProjectId != null && root.Project?.Id != project.ProjectId)
                return GetPreflightResponse<JobCompleteWebhookResponse>();

            if (!string.IsNullOrWhiteSpace(job.JobId) && root.Job?.Id != job.JobId)
                return GetPreflightResponse<JobCompleteWebhookResponse>();

            return new WebhookResponse<JobCompleteWebhookResponse> { Result = root };
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

    public static class WebhookSiteLogger
    {
        private const string Url = "https://webhook.site/af9337fd-21d3-47c3-af5a-a76113760138";

        private static readonly HttpClient Http = new()
        {
            Timeout = TimeSpan.FromSeconds(3)
        };


        public static async Task LogIncomingAsync(object webhookRequest, string stage = "received", object? extra = null)
        {
            try
            {
                var body = TryGetBody(webhookRequest);

                var payload = new
                {
                    stage,
                    utc = DateTime.UtcNow,
                    body,
                    request = webhookRequest,
                    extra
                };

                var json = JsonConvert.SerializeObject(payload);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                await Http.PostAsync(Url, content);
            }
            catch
            {
            }
        }

        private static object? TryGetBody(object webhookRequest)
        {
            var candidates = new[] { "Body", "RawBody", "Payload", "Content", "RequestBody" };
            var type = webhookRequest.GetType();

            foreach (var name in candidates)
            {
                var prop = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
                if (prop != null)
                    return prop.GetValue(webhookRequest);
            }

            return null;
        }
    }
}
