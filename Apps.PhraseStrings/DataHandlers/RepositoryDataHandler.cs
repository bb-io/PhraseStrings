using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Order;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers
{
    public class RepositoryDataHandler(InvocationContext invocationContext, [ActionParameter] AccountRequest account) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new RestRequest($"/v2/accounts/{account.AccoutnId}/repo_syncs", Method.Get);
            var jobs = await Client.Paginate<OrderResponse>(request);

            return jobs.Select(x => new DataSourceItem(x.Id, x.Message));
        }
    }
}
