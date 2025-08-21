using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.System;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList("Jobs")]
    public class JobActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {

        [Action("Search jobs", Description ="Searches jobs")]
        public async Task<ListJobsResponse> SearchJobs([ActionParameter] SearchJobsRequest input,
            [ActionParameter] ProjectRequest project)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs", Method.Get);

            if (!string.IsNullOrEmpty(input.Branch))
            {
                request.AddQueryParameter("branch", input.Branch);
            }

            if (!string.IsNullOrEmpty(input.OwnedBy))
            {
                request.AddQueryParameter("owned_by", input.OwnedBy);
            }

            if (!string.IsNullOrEmpty(input.AssignedTo))
            {
                request.AddQueryParameter("assigned_to", input.AssignedTo);
            }

            if (!string.IsNullOrEmpty(input.State))
            {
                request.AddQueryParameter("state", input.State);
            }

            if (input.UpdatedSince.HasValue)
            {
                var updatedSinceValue = input.UpdatedSince.Value
                    .ToUniversalTime()
                    .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                request.AddQueryParameter("updated_since", updatedSinceValue);
            }
            var jobs = await Client.Paginate<JobResponse>(request);
            return new ListJobsResponse { Jobs = jobs };

        }

        [Action("Create job", Description ="Creates job")]
        public async Task<CreateJobResponse> CreateJob([ActionParameter] CreateJobRequest input,
            [ActionParameter] ProjectRequest project)
        {
            var json = JsonConvert.SerializeObject(input, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs", Method.Post);
            request.AddStringBody(json, DataFormat.Json);

            var job = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);
            return job;

        }

        [Action("Get job", Description ="Gets job info")]
        public async Task<CreateJobResponse> GetJob([ActionParameter] JobRequest input,
            [ActionParameter] ProjectRequest project)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{input.JobId}", Method.Get);
            var job = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);

            return job;
        }

        [Action("Start job", Description = "Starts job and returns info")]
        public async Task<CreateJobResponse> StartJob([ActionParameter] JobRequest input,
           [ActionParameter] ProjectRequest project, [ActionParameter][Display("Branch")][DataSource(typeof(BranchDataHandler))] string? branch)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{input.JobId}/start", Method.Post);
            if (!string.IsNullOrEmpty(branch))
            {
                request.AddJsonBody(branch);
            }
            var job = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);

            return job;
        }

        [Action("Update job", Description = "Updates job's information")]
        public async Task<CreateJobResponse> UpdateJob(
            [ActionParameter] ProjectRequest project,
            [ActionParameter] JobRequest job,
            [ActionParameter] UpdateJobRequest update)
        {
            static string? NullIfEmpty(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

            var payload = new
            {
                branch = NullIfEmpty(update.Branch),
                name = NullIfEmpty(update.Name),
                briefing = NullIfEmpty(update.Briefing),
                due_date = update.DueDate,
                ticket_url = NullIfEmpty(update.TicketUrl)
            };

            var hasAnyValue = payload.AsDictionary().Values.Any(static v => v is not null);

            if (!hasAnyValue)
                throw new PluginMisconfigurationException("At least one optional input must be provided.");

            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{job.JobId}", Method.Patch)
                .WithJsonBody(payload, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);
        }

        [Action("Add keys to job", Description = "Adds keys to job")]
        public async Task<CreateJobResponse> AddKeysToJob([ActionParameter] JobRequest input,
           [ActionParameter] ProjectRequest project, [ActionParameter] AddkeysToJobRequest keys)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{input.JobId}/keys", Method.Post);
            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(keys.Branch))
            {
                body["branch"] = keys.Branch;
            }

            if (keys.Keys != null && keys.Keys.Count > 0)
            {
                body["translation_key_ids"] = keys.Keys;
            }

            if (body.Count > 0)
            {
                request.AddJsonBody(body);
            }

            var job = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);

            return job;
        }

        [Action("Add target locales to a job", Description = "Adds target locales to a job. Use 'Get project locales' action to obtain locale ID from ISO codes.")]
        public async Task<AddTargetLocaleToJobResponse> AddTargetLocaleToJob(
            [ActionParameter] ProjectRequest project,
            [ActionParameter] JobRequest job,
            [ActionParameter] AddTargetLocaleToJobRequest input)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{job.JobId}/locales", Method.Post)
                .WithJsonBody(input, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

            return await Client.ExecuteWithErrorHandling<AddTargetLocaleToJobResponse>(request);
        }


        [Action("Complete a job", Description = "Completes job and returns info")]
        public async Task<CreateJobResponse> CompleteJob([ActionParameter] JobRequest input,
           [ActionParameter] ProjectRequest project, [ActionParameter][Display("Branch")][DataSource(typeof(BranchDataHandler))] string? branch)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{input.JobId}/complete", Method.Post);
            if (!string.IsNullOrEmpty(branch))
            {
                request.AddJsonBody(branch);
            }
            var job = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);

            return job;
        }

        [Action("Reopen a job", Description = "Reopens job and returns info")]
        public async Task<CreateJobResponse> ReopenJob([ActionParameter] JobRequest input,
          [ActionParameter] ProjectRequest project, [ActionParameter][Display("Branch")][DataSource(typeof(BranchDataHandler))] string? branch)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{input.JobId}/reopen", Method.Post);
            if (!string.IsNullOrEmpty(branch))
            {
                request.AddJsonBody(branch);
            }
            var job = await Client.ExecuteWithErrorHandling<CreateJobResponse>(request);

            return job;
        }
    }
}


