using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Base;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.PhraseStrings.Webhooks.Handler
{
    public class JobCompletedHandler(InvocationContext invocationContext,
        [WebhookParameter(true)] ProjectRequest projectInput,
        [WebhookParameter(true)] WebhookJobRequest jobInput) : PhraseStringsWebhookHandler(invocationContext, projectInput), IAfterSubscriptionWebhookEventHandler<JobCompleteWebhookResponse>
    {
        protected override string SubscriptionEvents => "jobs:complete";

        public async Task<AfterSubscriptionEventResponse<JobCompleteWebhookResponse>> OnWebhookSubscribedAsync()
        {
            if (string.IsNullOrWhiteSpace(jobInput?.JobId))
                return null!;

            if (string.IsNullOrWhiteSpace(projectInput?.ProjectId))
                return null!;

            var request = new RestRequest($"/v2/projects/{projectInput.ProjectId}/jobs/{jobInput.JobId}", Method.Get);
            var jobInfo = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);

            if (jobInfo == null || string.IsNullOrWhiteSpace(jobInfo.State))
                return null!;

            if (!IsCompleted(jobInfo.State))
                return null!;

            var result = new JobCompleteWebhookResponse
            {
                Event = "jobs:complete",
                Message = "After subscription: job is already completed",
                Project = new WebhookProject
                {
                    Id = projectInput.ProjectId
                },
                Job = new WebhookJob
                {
                    Id = jobInfo.Id,
                    Name = jobInfo.Name,
                    Briefing = jobInfo.Briefing,
                    DueDate = jobInfo.DueDate,
                    State = jobInfo.State,
                    TicketUrl = jobInfo.TicketUrl,
                    CreatedAt = jobInfo.CreatedAt,
                    UpdatedAt = jobInfo.UpdatedAt
                }
            };

            return new AfterSubscriptionEventResponse<JobCompleteWebhookResponse>
            {
                Result = result
            };
        }

        private static bool IsCompleted(string state)
        {
            state = state.Trim();
            return state.Equals("completed", StringComparison.OrdinalIgnoreCase);
        }
    }
}
