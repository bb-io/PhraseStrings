using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Repository;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.Actions;

[ActionList("Repositories")]
public class RepositoryActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
{
    [Action("Search repositories", Description = "Search repository syncs for an account.")]
    public async Task<List<RepositoryResponse>> SearchRepositories(
        [ActionParameter] AccountRequest account,
        [ActionParameter] SearchRepositoriesRequest input)
    {
        return await GetRepositories(account.AccountId, input.IgnoreInactiveRepos == true);
    }

    [Action("Find repository", Description = "Find the first repository sync by project ID or repository name.")]
    public async Task<RepositoryResponse> FindRepository(
        [ActionParameter] AccountRequest account,
        [ActionParameter] FindRepositoryRequest input)
    {
        if (string.IsNullOrWhiteSpace(input.ProjectId) && string.IsNullOrWhiteSpace(input.RepositoryName))
            throw new PluginMisconfigurationException("Either Project ID or Repository name must be provided.");

        var repositories = await GetRepositories(account.AccountId, false);
        var repository = repositories.FirstOrDefault(repository =>
            (string.IsNullOrWhiteSpace(input.ProjectId) || repository.Project?.Id == input.ProjectId) &&
            (string.IsNullOrWhiteSpace(input.RepositoryName) || repository.RepoName.Contains(input.RepositoryName, StringComparison.OrdinalIgnoreCase)));

        if (repository == null)
            throw new PluginMisconfigurationException("No repository sync matched the provided search criteria.");

        return repository;
    }

    [Action("Export to code repository", Description = "Export project content to a code repository.")]
    public async Task<ImportResponse> ExportToCodeRepository([ActionParameter] AccountRequest account,
        [ActionParameter] RepositoryRequest repository)
    {
        var request = new RestRequest($"/v2/accounts/{account.AccountId}/repo_syncs/{repository.RepositoryId}/export", Method.Post);

        return await Client.ExecuteWithErrorHandling<ImportResponse>(request);
    }

    [Action("Import from code repository", Description = "Import project content from a code repository.")]
    public async Task<ImportResponse> ImportFromCodeRepository([ActionParameter] AccountRequest account,
        [ActionParameter] RepositoryRequest repository)
    {
        var request = new RestRequest($"/v2/accounts/{account.AccountId}/repo_syncs/{repository.RepositoryId}/import", Method.Post);

        return await Client.ExecuteWithErrorHandling<ImportResponse>(request);
    }

    private async Task<List<RepositoryResponse>> GetRepositories(string accountId, bool ignoreInactiveRepos)
    {
        const int perPage = 100;
        var allRepos = new List<RepositoryResponse>();
        var page = 1;

        while (true)
        {
            var request = new RestRequest($"/v2/accounts/{accountId}/repo_syncs", Method.Get)
                .AddQueryParameter("page", page.ToString())
                .AddQueryParameter("per_page", perPage.ToString());

            var pageResult =
                await Client.ExecuteWithErrorHandling<List<RepositoryResponse>>(request)
                ?? [];

            if (pageResult.Count == 0)
                break;

            if (ignoreInactiveRepos)
                pageResult.RemoveAll(x => x.Enabled == false);

            allRepos.AddRange(pageResult);

            if (pageResult.Count < perPage)
                break;

            page++;
        }

        return allRepos;
    }
}
