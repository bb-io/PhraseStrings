using Newtonsoft.Json;
using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Repository;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class RepositoryActionTests : TestBase
    {
        private RepositoryActions _actions => new(InvocationContext);

        [TestMethod]
        public async Task SearchRepositories_IsSuccess()
        {
            // Arrange
            var input = new SearchRepositoriesRequest { IgnoreInactiveRepos = true };
            var account = new AccountRequest { AccountId = "8134f0cd7ea179c246eb16e7be49b708" };

            // Act
            var result = await _actions.SearchRepositories(account, input);

            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ImportRepository_IsSuccess()
        {
            var result = await _actions.ImportFromCodeRepository(
                new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" },
                new RepositoryRequest { RepositoryId = "4e3d47862625700df4a0a33289bbb19e" });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ExportRepository_IsSuccess()
        {
            var result = await _actions.ExportToCodeRepository(
                new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" },
                new RepositoryRequest { RepositoryId = "4e3d47862625700df4a0a33289bbb19e" });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }
    }
}
