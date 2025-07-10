using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Screenshot;
using Apps.PhraseStrings.Model.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList]
    public class ScreenshotActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : PhraseStringsInvocable(invocationContext)
    {
        [Action("Upload screenshot", Description = "Uploads screenshot")]
        public async Task<ScreenshotResponse> UploadScreenshot([ActionParameter] ProjectRequest project,
            [ActionParameter] UploadScreenshotRequest  screenShot)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/screenshots", Method.Post);

            request.AddParameter("name", screenShot.Name);

            if (!string.IsNullOrEmpty(screenShot.Branch))
                request.AddParameter("branch", screenShot.Branch);

            if (!string.IsNullOrEmpty(screenShot.Description))
                request.AddParameter("description", screenShot.Description);

            using var stream = await fileManagementClient.DownloadAsync(screenShot.File);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var fileContents = await fileManagementClient.DownloadAsync(screenShot.File);
            request.AddFile("filename",fileBytes,screenShot.File.Name,screenShot.File.ContentType);

            return await Client.ExecuteWithErrorHandling<ScreenshotResponse>(request);
        }

        [Action("Create a screenshot marker", Description = "Creates a screenshot marker")]
        public async Task<ScreenshotResponse> CreateScreenshotMarker([ActionParameter] ProjectRequest project,
            [ActionParameter] CreateScreenshotMarkerRequest screenshot)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/screenshots/{screenshot.ScreenshotId}/markers", Method.Post);

            request.AddParameter("key_id", screenshot.KeyId);

            if (!string.IsNullOrEmpty(screenshot.Branch))
                request.AddParameter("branch", screenshot.Branch);

            if (!string.IsNullOrEmpty(screenshot.Branch))
                request.AddParameter("presentation", screenshot.Presentation);

            return await Client.ExecuteWithErrorHandling<ScreenshotResponse>(request);
        }
    }
}
