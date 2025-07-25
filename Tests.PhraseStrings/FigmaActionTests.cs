using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class FigmaActionTests : TestBase
    {
        private FigmaActions _actions => new(InvocationContext);

        [TestMethod]
        public async Task AddFigmaLinkTest_IsSuccess()
        {
            var request = new UploadFigmaLinkRequest
            {
                Url = "https://www.figma.com/design/wn14k5ppvMtdvYeH8Xu0z7/Ticket-Sale?node-id=4-186&t=XejJCWt248qUqfsU-1",
                KeyId = "514c14ab2c93aa334a566f354cd8b22a",
                //Branch = ""
            };
            var project = new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" };

            var response = await _actions.AddFigmaLink(project, request);

            var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
