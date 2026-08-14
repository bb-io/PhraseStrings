using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.PhraseStrings.Webhooks
{
    [PollingEventList]
    public class PollingList(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {
        private const string CompletedState = "completed";
        private const string JobCompletedEvent = "jobs:complete";

        [PollingEvent("On job completed (temporary)", Description = "Runs at specified intervals and checks whether the specified job has reached the completed state.")]
        public Task<PollingEventResponse<JobStatePollingMemory, JobCompleteWebhookResponse>> OnJobCompletedPolling(
            PollingEventRequest<JobStatePollingMemory> request,
            [PollingEventParameter] ProjectRequest project,
            [PollingEventParameter] JobRequest job)
            => HandleJobCompletedPolling(request, project, job);

        [PollingEvent("On repository sync failure", Description = "Runs when a repository sync import or export fails.")]
        public Task<PollingEventResponse<DateMemory, RepoSyncErrorBatch>> OnRepoSyncFailure(
            PollingEventRequest<DateMemory> request, [PollingEventParameter] RepoSyncRequest input)
            => HandleRepoSyncFailurePolling(request,input);

        private async Task<PollingEventResponse<JobStatePollingMemory, JobCompleteWebhookResponse>> HandleJobCompletedPolling(
            PollingEventRequest<JobStatePollingMemory> request, ProjectRequest project, JobRequest job)
        {
            if (string.IsNullOrWhiteSpace(project?.ProjectId))
                throw new PluginMisconfigurationException("Project ID is required. Specify the project of the job to watch and try again.");

            if (string.IsNullOrWhiteSpace(job?.JobId))
                throw new PluginMisconfigurationException("Job ID is required. Specify the job to watch and try again.");

            var jobRequest = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{job.JobId}", Method.Get);
            var jobInfo = await Client.ExecuteWithErrorHandling<CreateJobResponse>(jobRequest);

            var currentState = jobInfo?.State?.Trim();
            var memory = new JobStatePollingMemory
            {
                LastPollingTime = DateTime.UtcNow,
                LastState = currentState
            };
            
            var alreadyReported = IsCompleted(request.Memory?.LastState);

            if (jobInfo == null || !IsCompleted(currentState) || alreadyReported)
            {
                return new PollingEventResponse<JobStatePollingMemory, JobCompleteWebhookResponse>
                {
                    FlyBird = false,
                    Memory = memory,
                    Result = null
                };
            }

            return new PollingEventResponse<JobStatePollingMemory, JobCompleteWebhookResponse>
            {
                FlyBird = true,
                Memory = memory,
                Result = BuildJobCompletedResult(project.ProjectId, jobInfo)
            };
        }

        private static JobCompleteWebhookResponse BuildJobCompletedResult(string projectId, CreateJobResponse jobInfo)
            => new()
            {
                Event = JobCompletedEvent,
                Message = "Polling: job is completed",
                Project = new WebhookProject
                {
                    Id = jobInfo.Project?.Id ?? projectId,
                    Name = jobInfo.Project?.Name,
                    MainFormat = jobInfo.Project?.MainFormat,
                    CreatedAt = jobInfo.Project?.CreatedAt,
                    UpdatedAt = jobInfo.Project?.UpdatedAt
                },
                Branch = jobInfo.Branch?.Name == null ? null : new WebhookBranch { Name = jobInfo.Branch.Name },
                Job = new WebhookJob
                {
                    Id = jobInfo.Id,
                    Name = jobInfo.Name,
                    Briefing = jobInfo.Briefing,
                    DueDate = jobInfo.DueDate,
                    State = jobInfo.State,
                    TicketUrl = jobInfo.TicketUrl,
                    CreatedAt = jobInfo.CreatedAt,
                    UpdatedAt = jobInfo.UpdatedAt,
                    Owner = jobInfo.Owner == null ? null : new WebhookUser
                    {
                        Id = jobInfo.Owner.Id,
                        Username = jobInfo.Owner.Username,
                        Name = jobInfo.Owner.Name
                    }
                }
            };

        private static bool IsCompleted(string? state)
            => !string.IsNullOrWhiteSpace(state) && state.Trim().Equals(CompletedState, StringComparison.OrdinalIgnoreCase);
        
        private async Task<PollingEventResponse<DateMemory, RepoSyncErrorBatch>> HandleRepoSyncFailurePolling(PollingEventRequest<DateMemory> request, RepoSyncRequest input)
        {

            if (request.Memory == null)
            {
                return new PollingEventResponse<DateMemory, RepoSyncErrorBatch>
                {
                    FlyBird = false,
                    Memory = new DateMemory { LastInteractionDate = DateTime.UtcNow },
                    Result = null
                };
            }

            var errorsFound = new List<RepoSyncError>();

            var lastChecked = request.Memory?.LastInteractionDate ?? DateTime.UtcNow;

            var repList = new RestRequest($"/v2/accounts/{input.AccountId}/repo_syncs", Method.Get);
            var allSyncs = await Client.ExecuteWithErrorHandling<List<RepoSyncDto>>(repList);

            foreach (var sync in allSyncs)
            {
                var newestTimestamp = new[] { sync.LastImportAt, sync.LastExportAt }
                    .Where(ts => ts.HasValue)
                    .Max() ?? DateTime.MinValue;

                if (newestTimestamp <= lastChecked)
                    continue;

                var repHistory = new RestRequest($"/v2/accounts/{input.AccountId}/repo_syncs/{sync.Id}/events", Method.Get);
                var history = await Client.ExecuteWithErrorHandling<List<RepoSyncHistoryDto>>(repHistory);

                var newErrors = history
                    .Where(e => e.Status == "failure" && e.CreatedAt > lastChecked)
                    .SelectMany(e => e.Errors.Select(errToken => new RepoSyncError
                    {
                        SyncId = sync.Id,
                        ProjectId = sync.Project.Id,
                        ProjectName = sync.Project.Name,
                        Provider = sync.Provider,
                        RepoName = sync.RepoName,
                        LastImportAt = sync.LastImportAt,
                        LastExportAt = sync.LastExportAt,
                        EventType = e.Type,
                        EventStatus = e.Status,
                        Timestamp = e.CreatedAt,
                        Message = errToken.Type == JTokenType.String
                            ? errToken.ToString()
                            : errToken["message"]?.ToString() ?? errToken.ToString()
                    }));

                errorsFound.AddRange(newErrors);
            }

            if (!errorsFound.Any())
            {
                return new PollingEventResponse<DateMemory, RepoSyncErrorBatch>
                {
                    FlyBird = false,
                    Memory = new DateMemory { LastInteractionDate = lastChecked }
                };
            }

            var maxNew = errorsFound.Max(e => e.Timestamp);
            var newMemory = new DateMemory { LastInteractionDate = maxNew };

            return new PollingEventResponse<DateMemory, RepoSyncErrorBatch>
            {
                FlyBird = true,
                Memory = newMemory,
                Result = new RepoSyncErrorBatch(errorsFound)
            };
        }
    }
}
