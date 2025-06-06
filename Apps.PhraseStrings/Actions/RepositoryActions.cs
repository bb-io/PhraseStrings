using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Repository;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList]
    public class RepositoryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : PhraseStringsInvocable(invocationContext)
    {
        [Action("Export to code repository", Description = "Exports to code repository")]
        public async Task<ImportResponse> ExportToCodeRepository([ActionParameter] AccountRequest account,
            [ActionParameter] RepositoryRequest repository)
        {
            var request = new RestRequest($"/accounts/{account.AccoutnId}/repo_syncs/{repository.RepositoryId}/export", Method.Post);

            return await Client.ExecuteWithErrorHandling<ImportResponse>(request);
        }

        [Action("Import from code repository", Description = "Imports from code repository")]
        public async Task<ImportResponse> ImprotsFromCOdeRepository([ActionParameter] AccountRequest account,
            [ActionParameter] RepositoryRequest repository)
        {
            var request = new RestRequest($"/accounts/{account.AccoutnId}/repo_syncs/{repository.RepositoryId}/import", Method.Post);

            return await Client.ExecuteWithErrorHandling<ImportResponse>(request);
        }

    }
}
