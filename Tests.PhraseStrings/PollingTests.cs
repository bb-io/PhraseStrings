using Apps.PhraseStrings.Webhooks;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class PollingTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task OnRepoSyncFailure_IsSuccess(InvocationContext context)
        {
            var polling = new PollingList(context);

            var request = new PollingEventRequest<DateMemory>
            {
                //Memory = new DateMemory { LastInteractionDate = DateTime.UtcNow.AddDays(-20) }
            };
            var input = new RepoSyncRequest
            {
                AccountId = "dd76e8ff50005091bd5c1757c2b6e893"
            };
            var response = await polling.OnRepoSyncFailure(request, input);

            PrintResult(response);
            Assert.IsNotNull(response);
        }
    }
}
