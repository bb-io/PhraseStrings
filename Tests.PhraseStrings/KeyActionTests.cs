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
        [TestMethod]
        public async Task SearchKeys_IsSuccess()
        {
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.SearchKeys(new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" },
                new SearchKeysRequest { Tags = ["test", "rogue"] });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetKeyByName_IsSuccess()
        {
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.GetKeyByName(
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
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.CreateKey(new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest { Name="Key created localy"});

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task UpdateKey_IsSuccess()
        {
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.UpdateKey(new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest { Name = "Key created localy(updated 2)" }, new KeyRequest { KeyId= "7e1fc73eb9c2c401b89d12579e4e4b13" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddTagsTokeys_IsSuccess()
        {
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.AddtagsToKeys(new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, 
                new AddTagsToKeysRequest {Query= "ids:7e1fc73eb9c2c401b89d12579e4e4b13", Tags= "Added tag, Hello" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task RemoveTagsTokeys_IsSuccess()
        {
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.RemovetagsToKeys(new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new AddTagsToKeysRequest { Query = "ids:7e1fc73eb9c2c401b89d12579e4e4b13", Tags = "Hello" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
