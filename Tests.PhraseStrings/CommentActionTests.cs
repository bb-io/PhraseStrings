using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Comment;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class CommentActionTests : TestBase
    {
        [TestMethod]
        public async Task AddCommentToKey_IsSuccess()
        {
            var action = new CommentActions(InvocationContext, FileManager);

            var response = await action.AddCommentToKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateCommentRequest
                {
                    Message = "Test comment to the key"
                },
                new KeyRequest { KeyId = "c9de884de06dafd683e65d3c2f2fa38c" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddCommentToJob_IsSuccerss()
        {
            var action = new CommentActions(InvocationContext, FileManager);

            var response = await action.AddCommentToJob(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateCommentRequest
                {
                    Message = "Test comment to the key(one more)"
                },
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
