using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.Handlers;
public class ProjectDataHandler(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new RestRequest("/v2/projects", Method.Get);
        var projects = await Client.Paginate<ProjectResponse>(request);

        return projects.Select(x=>new DataSourceItem ( x.Id, x.Name));
    }
}
