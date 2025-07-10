using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Team;
using Apps.PhraseStrings.Model.User;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class UserAndTeamTests : TestBase
{
    private UserAndTeamActions Actions => new(InvocationContext);

    [TestMethod]
    public async Task GetUserByEmail_IsSuccess()
    {
        var response = await Actions.GetUserByEmail(new GetUserByEmailRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            Email = "alex.terekhov@blackbird.io",
        });

        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetTeamByName_ExactMatchWorks()
    {
        var response = await Actions.GetTeamByName(new GetTeamByNameRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            TeamName = "sample team",
        });

        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetTeamByName_ContainsWorks()
    {
        var response = await Actions.GetTeamByName(new GetTeamByNameRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            TeamName = "sample",
            UseContains = true,
        });

        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        Assert.IsNotNull(response);
    }
}
