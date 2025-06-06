using Apps.PhraseStrings.Model.Account;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers
{
    public class AccountDataHandler(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new RestRequest("/v2/accounts", Method.Get);
            var projects = await Client.Paginate<AccountResponse>(request);

            return projects.Select(x => new DataSourceItem(x.Id, x.Name));
        }
    }
}
