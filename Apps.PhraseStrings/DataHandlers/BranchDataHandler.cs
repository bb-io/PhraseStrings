using Apps.PhraseStrings.Model.Branch;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers
{
    public class BranchDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectRequest project) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/branches", Method.Get);
            var jobs = await Client.Paginate<BranchResponse>(request);

            return jobs.Select(x => new DataSourceItem(x.Name, x.Name));
        }
    }
}