using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class KeyActionTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task SearchKeys_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.SearchKeys(
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" },
                new SearchKeysRequest { Tags = ["test", "rogue"] });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task GetKeyByName_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.GetKeyByName(
                new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
                new BranchRequest { },
                new KeyNameRequest { KeyName = "dashboard_welcome_message" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task CreateKey_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.CreateKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest { Name="Key created localy"});

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task UpdateKey_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.UpdateKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest {Description="new description 2"},
                new KeyRequest { KeyId= "514c14ab2c93aa334a566f354cd8b22a" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task AddTagsTokeys_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.AddtagsToKeys(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, 
                new AddTagsToKeysRequest { Query = "ids:7e1fc73eb9c2c401b89d12579e4e4b13", Tags = "Added tag, Hello" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task RemoveTagsTokeys_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.RemovetagsToKeys(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new AddTagsToKeysRequest { Query = "ids:7e1fc73eb9c2c401b89d12579e4e4b13", Tags = "Hello" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task LinkChildKeys_IsSuccess(InvocationContext context)
        {
            var actions = new KeyActions(context);
            var response = await actions.LinkChildKeys(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new KeyRequest { KeyId = "514c14ab2c93aa334a566f354cd8b22a" },
                new ChildrenKeyIdsRequest { KeyIds = ["7e1fc73eb9c2c401b89d12579e4e4b13"] }
            );

            PrintResult(response);
            Assert.IsNotEmpty(response.KeyIds);
        }
    }
}
