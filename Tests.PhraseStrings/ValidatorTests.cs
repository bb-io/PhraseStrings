using Apps.PhraseStrings.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class ConnectionValidatorTests : TestBaseMultipleConnections
{
    [TestMethod, ContextDataSource]
    public async Task ValidateConnection_ValidData_ShouldBeSuccessful(InvocationContext context)
    {
        var validator = new ConnectionValidator();

        var tasks = CredentialGroups.Select(x => validator.ValidateConnection(x, CancellationToken.None).AsTask());
        var results = await Task.WhenAll(tasks);
        Assert.IsTrue(results.All(x => x.IsValid));
    }

    [TestMethod]
    public async Task ValidateConnection_InvalidData_ShouldFail(InvocationContext context)
    {
        var validator = new ConnectionValidator();
        var newCreds = CredentialGroups.First().Select(x => new AuthenticationCredentialsProvider(x.KeyName, x.Value + "_incorrect"));

        await Assert.ThrowsExactlyAsync<Exception>(async () =>
            await validator.ValidateConnection(newCreds, CancellationToken.None)
        );
    }
}