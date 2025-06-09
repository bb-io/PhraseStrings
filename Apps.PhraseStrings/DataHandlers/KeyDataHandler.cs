using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers
{
    public class KeyDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectRequest project) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/keys", Method.Get);
            var jobs = await Client.Paginate<KeyResponse>(request);

            return jobs.Select(x => new DataSourceItem(x.Id, x.Name));
        }
    }
}