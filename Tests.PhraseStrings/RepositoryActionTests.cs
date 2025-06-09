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
        public async Task ImportRepository_IsSuccess()
        {
            var action = new RepositoryActions(InvocationContext, FileManager);
            var result = await action.ImprotsFromCOdeRepository(new AccountRequest { },
                new RepositoryRequest { RepositoryId = "" });
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ExportRepository_IsSuccess()
        {
            var action = new RepositoryActions(InvocationContext, FileManager);
            var result = await action.ExportToCodeRepository(new AccountRequest { },
                new RepositoryRequest { RepositoryId = "" });
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(result);
        }
    }
}
