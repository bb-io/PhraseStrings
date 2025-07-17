using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.PhraseStrings.Webhooks
{
    [PollingEventList]
    public class PollingList(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {
        [PollingEvent("On repository sync failure", Description = "Triggers when a repo sync import or export fails")]
        public Task<PollingEventResponse<DateMemory, RepoSyncErrorBatch>> OnRepoSyncFailure(
            PollingEventRequest<DateMemory> request, [PollingEventParameter] RepoSyncRequest input)
            => HandleRepoSyncFailurePolling(request,input);

        private async Task<PollingEventResponse<DateMemory, RepoSyncErrorBatch>> HandleRepoSyncFailurePolling(
        PollingEventRequest<DateMemory> request, RepoSyncRequest input)
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
