using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Comment;
using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class CommentActionTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task AddCommentToKey_IsSuccess(InvocationContext context)
        {
            var actions = new CommentActions(context);
            var response = await actions.AddCommentToKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateCommentRequest { Message = "Test comment to the key" },
                new KeyRequest { KeyId = "c9de884de06dafd683e65d3c2f2fa38c" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task AddCommentToJob_IsSuccerss(InvocationContext context)
        {
            var actions = new CommentActions(context);
            var response = await actions.AddCommentToJob(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateCommentRequest { Message = "Test comment to the key(one more)" },
                new JobRequest { JobId = "616f70f52101f2e219f0ef8192320871" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }
    }
}
