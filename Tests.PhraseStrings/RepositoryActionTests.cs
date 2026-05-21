using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Repository;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class RepositoryActionTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task SearchRepositories_IsSuccess(InvocationContext context)
        {
            // Arrange
            var actions = new RepositoryActions(context);
            var input = new SearchRepositoriesRequest { IgnoreInactiveRepos = true };
            var account = new AccountRequest { AccountId = "8134f0cd7ea179c246eb16e7be49b708",  };

            // Act
            var result = await actions.SearchRepositories(account, input);

            // Assert
            PrintResult(result);
            Assert.IsNotNull(result);
        }

        [TestMethod, ContextDataSource]
        public async Task FindRepository_IsSuccess(InvocationContext context)
        {
            var actions = new RepositoryActions(context);
            var account = new AccountRequest { AccountId = "8134f0cd7ea179c246eb16e7be49b708" };
            var repositories = await actions.SearchRepositories(account, new SearchRepositoriesRequest());
            var sourceRepository = repositories.First(x => !string.IsNullOrWhiteSpace(x.RepoName));

            var result = await actions.FindRepository(account, new FindRepositoryRequest
            {
                RepositoryName = sourceRepository.RepoName
            });

            PrintResult(result);
            Assert.IsNotNull(result);
        }

        [TestMethod, ContextDataSource]
        public async Task ImportRepository_IsSuccess(InvocationContext context)
        {
            var actions = new RepositoryActions(context);
            var result = await actions.ImportFromCodeRepository(
                new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" },
                new RepositoryRequest { RepositoryId = "4e3d47862625700df4a0a33289bbb19e" });

            PrintResult(result);
            Assert.IsNotNull(result);
        }

        [TestMethod, ContextDataSource]
        public async Task ExportRepository_IsSuccess(InvocationContext context)
        {
            var actions = new RepositoryActions(context);
            var result = await actions.ExportToCodeRepository(
                new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" },
                new RepositoryRequest { RepositoryId = "4e3d47862625700df4a0a33289bbb19e" });

            PrintResult(result);
            Assert.IsNotNull(result);
        }
    }
}
