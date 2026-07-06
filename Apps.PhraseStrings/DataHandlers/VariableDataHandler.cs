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
        var variables = new List<VariableResponse>();
        var page = 1;

        while (true)
        {
            var request = new RestRequest("/v2/projects/{projectId}/variables", Method.Get)
                .AddUrlSegment("projectId", project.ProjectId)
                .AddQueryParameter("page", page)
                .AddQueryParameter("per_page", 50);

            var pageVariables = await Client.ExecuteWithErrorHandling<List<VariableResponse>>(request);
            if (pageVariables.Count == 0)
                break;

            variables.AddRange(pageVariables);
            page++;
        }

        return variables.Select(variable => new DataSourceItem(variable.Name, variable.Name));
    }
}
