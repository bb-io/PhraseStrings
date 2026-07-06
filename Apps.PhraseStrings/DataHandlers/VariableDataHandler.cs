using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Variable;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers;

public class VariableDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] ProjectRequest project)
    : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new RestRequest($"/v2/projects/{project.ProjectId}/variables", Method.Get);
        var variables = await Client.Paginate<VariableResponse>(request);

        return variables.Select(variable => new DataSourceItem(variable.Name, variable.Name));
    }
}
