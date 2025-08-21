using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class KeyActionTests : TestBase
    {
        private KeyActions _actions => new(InvocationContext);
        [TestMethod]
        public async Task SearchKeys_IsSuccess()
        {
            var response = await _actions.SearchKeys(
                new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" },
                new SearchKeysRequest { Tags = ["test", "rogue"] });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetKeyByName_IsSuccess()
        {
            var response = await _actions.GetKeyByName(
                new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
                new BranchRequest { },
                new KeyNameRequest { KeyName = "dashboard_welcome_message" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreateKey_IsSuccess()
        {
            var response = await _actions.CreateKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest { Name="Key created localy"});

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task UpdateKey_IsSuccess()
        {
            var response = await _actions.UpdateKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest {Description="new description 2"},
                new KeyRequest { KeyId= "514c14ab2c93aa334a566f354cd8b22a" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddTagsTokeys_IsSuccess()
        {
            var response = await _actions.AddtagsToKeys(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, 
                new AddTagsToKeysRequest { Query = "ids:7e1fc73eb9c2c401b89d12579e4e4b13", Tags = "Added tag, Hello" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task RemoveTagsTokeys_IsSuccess()
        {
            var response = await _actions.RemovetagsToKeys(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new AddTagsToKeysRequest { Query = "ids:7e1fc73eb9c2c401b89d12579e4e4b13", Tags = "Hello" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task LinkChildKeys_IsSuccess()
        {
            var response = await _actions.LinkChildKeys(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new KeyRequest { KeyId = "514c14ab2c93aa334a566f354cd8b22a" },
                new ChildrenKeyIdsRequest { KeyIds = ["7e1fc73eb9c2c401b89d12579e4e4b13"] }
            );

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);

            Console.WriteLine(json);
            Assert.IsNotEmpty(response.KeyIds);
        }
    }
}
