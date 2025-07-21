using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Repository;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class RepositoryActionTests : TestBase
    {
        [TestMethod]
        public async Task SearchRepositories_IsSuccess()
        {
            var action = new RepositoryActions(InvocationContext, FileManager);
            var result = await action.SearchRepositories(new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" });
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ImportRepository_IsSuccess()
        {
            var action = new RepositoryActions(InvocationContext, FileManager);
            var result = await action.ImprotsFromCOdeRepository(new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" },
                new RepositoryRequest { RepositoryId = "4e3d47862625700df4a0a33289bbb19e" });
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ExportRepository_IsSuccess()
        {
            var action = new RepositoryActions(InvocationContext, FileManager);
            var result = await action.ExportToCodeRepository(new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" },
                new RepositoryRequest { RepositoryId = "4e3d47862625700df4a0a33289bbb19e" });
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(result);
        }
    }
}
