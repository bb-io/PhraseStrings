using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Screenshot;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions;

[ActionList("Screenshots")]
public class ScreenshotActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
    : PhraseStringsInvocable(invocationContext)
{
    [Action("Upload screenshot", 
        Description = "Uploads screenshot. Use a 'Mark screenshot' action to connect keys to the uploaded screenshot")]
    public async Task<ScreenshotResponse> UploadScreenshot(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] UploadScreenshotRequest screenShot)
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

        request.AddFile("filename", fileBytes, screenShot.File.Name, screenShot.File.ContentType);

        var response = await Client.ExecuteWithErrorHandling<ScreenshotDtoResponse>(request);
        return new(response);
    }

    [Action("Mark screenshot (link to key)", 
        Description = "Creates a connection between a key and a screenshot, so screenshot will be shown in the editor")]
    public async Task<ScreenshotResponse> CreateScreenshotMarker(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] CreateScreenshotMarkerRequest screenshot)
    {
        var request = new RestRequest($"/v2/projects/{project.ProjectId}/screenshots/{screenshot.ScreenshotId}/markers", Method.Post);

        request.AddParameter("key_id", screenshot.KeyId);

        if (!string.IsNullOrEmpty(screenshot.Branch))
            request.AddParameter("branch", screenshot.Branch);

        if (!string.IsNullOrEmpty(screenshot.Branch))
            request.AddParameter("presentation", screenshot.Presentation);

        var response = await Client.ExecuteWithErrorHandling<ScreenshotDtoResponse>(request);
        return new(response);
    }

    [Action("Get uploaded screenshot", Description = "Gets a screenshot by its ID or name")]
    public async Task<ScreenshotResponse> GetScreenshotByIdOrName(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] GetScreenshotRequest screenshotInput)
    {
        if (string.IsNullOrEmpty(screenshotInput.ScreenshotID) && string.IsNullOrEmpty(screenshotInput.ScreenshotName))
            throw new PluginMisconfigurationException("Either screenshot ID or name must be provided");

        var listRequest = new RestRequest($"/v2/projects/{project.ProjectId}/screenshots", Method.Get);
        var listResponse = await Client.Paginate<ScreenshotDtoResponse>(listRequest);

        var response = new ScreenshotDtoResponse();

        if (!string.IsNullOrEmpty(screenshotInput.ScreenshotID))
            response = listResponse.FirstOrDefault(s => s.Id == screenshotInput.ScreenshotID) ?? new ScreenshotDtoResponse();

        if (!string.IsNullOrEmpty(screenshotInput.ScreenshotName))
        {
            response = listResponse
                .FirstOrDefault(s => s.Name.Equals(screenshotInput.ScreenshotName, StringComparison.OrdinalIgnoreCase)) ?? 
                new ScreenshotDtoResponse();
        }

        return new(response);
    }
}
