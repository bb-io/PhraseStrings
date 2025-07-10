using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.DataHandlers;
public class UserDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectRequest project) : PhraseStringsInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(project.ProjectId))
            throw new PluginMisconfigurationException("Add a Project ID to see a list of users connected to a project.");

        var projectRequest = new RestRequest($"/v2/projects/{project.ProjectId}", Method.Get);
        var projectResponse = await Client.ExecuteWithErrorHandling<ProjectResponse>(projectRequest);

        var usersRequest = new RestRequest($"/v2/accounts/{projectResponse.Account.Id}/members", Method.Get);
        var users = await Client.Paginate<UserResponse>(usersRequest);

        users = users
            .Where(u => u.Projects.Any(p => p.Id == project.ProjectId))
            .ToList();

        if (!string.IsNullOrEmpty(context.SearchString))
            users = users
                .Where(u => u.Username.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase)
                    || u.Email.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .ToList();

        return users.Select(u => new DataSourceItem(u.Id, $"{u.Username} ({u.Email})"));
    }
}
