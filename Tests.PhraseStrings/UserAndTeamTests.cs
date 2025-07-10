using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.User;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class UserAndTeamTests : TestBase
{
    private UserAndTeamActions _actions => new(InvocationContext, FileManager);

    [TestMethod]
    public async Task GetUserByEmail_IsSuccess()
    {
        var response = await _actions.GetUserByEmail(new GetUserByEmailRequest
        {
            AccountId = "851841f538f3e05cd437913851078076",
            Email = "alex.terekhov@blackbird.io",
        });

        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        Assert.IsNotNull(response);
    }
}
