using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Screenshot;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class ScreenshotTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task UploadScreenshot_IsSuccess(InvocationContext context)
        {
            var action = new ScreenshotActions(context, FileManager);

            var response = await action.UploadScreenshot(
                new ProjectRequest
                {
                    ProjectId = "52ea432ad1debbf8e09cdf344998167d"
                },
                new UploadScreenshotRequest
                {
                    Name = "Test Screenshot",
                    Description = "This is a test screenshot",
                    File = new FileReference
                    {
                        Name = "screen.png"
                    }
                });

            PrintResult(response);
        }

        [TestMethod, ContextDataSource]
        public async Task CreateMarkerScreenshot_IsSuccess(InvocationContext context)
        {
            var action = new ScreenshotActions(context, FileManager);

            var response = await action.CreateScreenshotMarker(
                new ProjectRequest
                {
                    ProjectId = "52ea432ad1debbf8e09cdf344998167d"
                },
                new CreateScreenshotMarkerRequest
                {
                    KeyId = "514c14ab2c93aa334a566f354cd8b22a",
                    ScreenshotId = "bbc00b201531ef098b5b968cb7ef2bc2"
                });

            PrintResult(response);
        }

        [TestMethod, ContextDataSource]
        public async Task GetScreenshot_IsSuccess(InvocationContext context)
        {
            var action = new ScreenshotActions(context, FileManager);
            var response = await action.GetScreenshotByIdOrName(
                new ProjectRequest
                {
                    ProjectId = "d562a2ad202e4ab626b0764576660917"
                },
                new GetScreenshotRequest
                {
                    ScreenshotName = "sample_screenshot (from Lark request #recuQx6pkRmD87)"
                });
            
            PrintResult(response);
        }
    }
}
