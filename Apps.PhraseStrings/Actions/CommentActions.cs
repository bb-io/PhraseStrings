using Apps.PhraseStrings.Model.Comment;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList("Comments")]
    public class CommentActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {
        [Action("Add comment to a key", Description = "Adds comment to a key")]
        public async Task<AddCommentToKeyResponse> AddCommentToKey([ActionParameter] ProjectRequest project,
            [ActionParameter] CreateCommentRequest input,
            [ActionParameter] KeyRequest key)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys/{key.KeyId}/comments", Method.Post);

            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(input.Message))
                body["message"] = input.Message;

            if (!string.IsNullOrEmpty(input.Branch))
                body["branch"] = input.Branch;

            if (input.Locales != null && input.Locales.Any())
                body["locale_ids"] = input.Locales;

            if (body.Count > 0)
                request.AddJsonBody(body);


            return await Client.ExecuteWithErrorHandling<AddCommentToKeyResponse>(request);           
        }

        [Action("Add comment to a job", Description = "Adds comment to a job")]
        public async Task<AddCommentToKeyResponse> AddCommentToJob([ActionParameter] ProjectRequest project,
            [ActionParameter] CreateCommentRequest input,
            [ActionParameter] JobRequest job)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/jobs/{job.JobId}/comments", Method.Post);

            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(input.Message))
                body["message"] = input.Message;

            if (!string.IsNullOrEmpty(input.Branch))
                body["branch"] = input.Branch;

            if (body.Count > 0)
                request.AddJsonBody(body);


            return await Client.ExecuteWithErrorHandling<AddCommentToKeyResponse>(request);
        }
    }
}
