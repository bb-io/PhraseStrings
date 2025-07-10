using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Screenshot;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class ScreenshotTests :TestBase
    {
        [TestMethod]
        public async Task UploadScreenshot_IsSuccess()
        {
            var action = new ScreenshotActions(InvocationContext, FileManager);

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

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
        }

        [TestMethod]
        public async Task CreateMarkerScreenshot_IsSuccess()
        {
            var action = new ScreenshotActions(InvocationContext, FileManager);

            var response = await action.CreateScreenshotMarker(
                new ProjectRequest
                {
                    ProjectId = "52ea432ad1debbf8e09cdf344998167d"
                },
                new CreateScreenshotMarkerRequest
                {
                    KeyId= "514c14ab2c93aa334a566f354cd8b22a",
                    ScreenshotId= "bbc00b201531ef098b5b968cb7ef2bc2"
                });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
