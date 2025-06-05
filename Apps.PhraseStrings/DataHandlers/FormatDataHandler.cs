using Apps.PhraseStrings.Model;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers
{
    public class FormatDataHandler(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new RestRequest("/v2/formats", Method.Get);
            var projects = await Client.ExecuteWithErrorHandling<List<FileFormatResponse>>(request);

            return projects.Select(x => new DataSourceItem(x.ApiName, x.Name));
        }
    }
}