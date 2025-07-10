using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions;

[ActionList]
public class UserAndTeamActions(InvocationContext invocationContext,IFileManagementClient fileManagementClient) : PhraseStringsInvocable(invocationContext)
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
}
