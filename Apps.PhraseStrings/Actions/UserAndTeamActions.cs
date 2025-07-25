using Apps.PhraseStrings.Model.Team;
using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.PhraseStrings.Actions;

[ActionList("Users and teams")]
public class UserAndTeamActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
{
    [Action("Get user by email", Description = "Returns user details")]
    public async Task<UserResponse> GetUserByEmail([ActionParameter] GetUserByEmailRequest input)
    {
        var usersRequest = new RestRequest($"/v2/accounts/{input.AccountId}/members", Method.Get);
        var users = await Client.Paginate<UserResponse>(usersRequest);

        var userFound = users
            .Where(u => u.Email.Equals(input.Email, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();

        if (userFound == null)
            throw new PluginMisconfigurationException($"User with specified email was not found in selected account.");

        return userFound;
    }

    [Action("Get team by name", Description = "Returns team details")]
    public async Task<TeamResponse> GetTeamByName([ActionParameter] GetTeamByNameRequest input)
    {
        var teamsRequest = new RestRequest($"/v2/accounts/{input.AccountId}/teams", Method.Get);
        var teams = await Client.Paginate<TeamResponse>(teamsRequest);

        var teamFound = teams
            .Where(t => input.UseContains == true
                ? t.Name.Contains(input.TeamName, StringComparison.OrdinalIgnoreCase)
                : t.Name.Equals(input.TeamName, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();

        if (teamFound == null)
            throw new PluginMisconfigurationException($"Team with specified name was not found in selected account.");

        return teamFound;
    }
}
