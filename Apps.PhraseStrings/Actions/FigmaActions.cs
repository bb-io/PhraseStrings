using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList("Figma")]
    public class FigmaActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {

        [Action("Add Figma link to key", Description = "Adds Figma link to a specified key")]
        public async Task<FigmaAttachmentResponse> AddFigmaLink([ActionParameter] ProjectRequest project,
            [ActionParameter] UploadFigmaLinkRequest figmaAttachment)
        {
            var createRequest = new RestRequest($"/v2/projects/{project.ProjectId}/figma_attachments", Method.Post);

            if (!string.IsNullOrEmpty(figmaAttachment.Branch))
                createRequest.AddQueryParameter("branch", figmaAttachment.Branch);

            createRequest.AddJsonBody(new
            {
                url = figmaAttachment.Url
            });

            var createResponse = await Client.ExecuteWithErrorHandling<FigmaAttachmentResponse>(createRequest);

            var attachRequest = new RestRequest($"/v2/projects/{project.ProjectId}/figma_attachments/{createResponse.Id}/keys", Method.Post);

            if (!string.IsNullOrEmpty(figmaAttachment.Branch))
                attachRequest.AddQueryParameter("branch", figmaAttachment.Branch);

            attachRequest.AddJsonBody(new
            {
                id = figmaAttachment.KeyId
            });

            var attachResponse = await Client.ExecuteWithErrorHandling(attachRequest);
            if (!attachResponse.IsSuccessful)
                throw new PluginApplicationException($"Failed to attach Figma link to key: {attachResponse.Content}");

            return createResponse;
        }
    }
}
