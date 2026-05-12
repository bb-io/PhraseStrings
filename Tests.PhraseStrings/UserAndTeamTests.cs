using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Team;
using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class UserAndTeamTests : TestBaseMultipleConnections
{
    [TestMethod, ContextDataSource]
    public async Task GetUserByEmail_IsSuccess(InvocationContext context)
    {
        var actions = new UserAndTeamActions(context);
        var response = await actions.GetUserByEmail(new GetUserByEmailRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            Email = "alex.terekhov@blackbird.io",
        });

        PrintResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod, ContextDataSource]
    public async Task GetTeamByName_ExactMatchWorks(InvocationContext context)
    {
        var actions = new UserAndTeamActions(context);
        var response = await actions.GetTeamByName(new GetTeamByNameRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            TeamName = "sample team",
        });

        PrintResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod, ContextDataSource]
    public async Task GetTeamByName_ContainsWorks(InvocationContext context)
    {
        var actions = new UserAndTeamActions(context);
        var response = await actions.GetTeamByName(new GetTeamByNameRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            TeamName = "sample",
            UseContains = true,
        });

        PrintResult(response);
        Assert.IsNotNull(response);
    }
}
