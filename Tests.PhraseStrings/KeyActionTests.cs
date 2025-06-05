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
            var response = await action.SearchKeys(new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new SearchKeysRequest { });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreatehKey_IsSuccess()
        {
            var action = new KeyActions(InvocationContext, FileManager);
            var response = await action.CreateKey(new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateKeyRequest { Name="Key created localy"});

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task UpdatehKey_IsSuccess()
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
