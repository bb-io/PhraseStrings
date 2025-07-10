using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Team;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers;
public class TeamDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectRequest project) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(project.ProjectId))
            throw new PluginMisconfigurationException("Add a Project ID to see a list of teams connected to a project.");

        var projectRequest = new RestRequest($"/v2/projects/{project.ProjectId}", Method.Get);
        var projectResponse = await Client.ExecuteWithErrorHandling<ProjectResponse>(projectRequest);

        var teamsRequest = new RestRequest($"/v2/accounts/{projectResponse.Account.Id}/teams", Method.Get);
        var teams = await Client.Paginate<TeamResponse>(teamsRequest);

        teams = teams
            .Where(t => t.Projects.Any(p => p.Id == project.ProjectId))
            .ToList();

        if (!string.IsNullOrEmpty(context.SearchString))
            teams = teams
                .Where(t => t.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .ToList();

        return teams.Select(t => new DataSourceItem(t.TeamId, t.Name));
    }
}
