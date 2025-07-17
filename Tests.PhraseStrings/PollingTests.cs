using Apps.PhraseStrings.Webhooks;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Polling;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class PollingTests : TestBase
    {
        [TestMethod]
        public async Task OnRepoSyncFailure_IsSuccess()
        {
            var polling = new PollingList(InvocationContext);

            var request = new PollingEventRequest<DateMemory>
            {
                //Memory = new DateMemory { LastInteractionDate = DateTime.UtcNow.AddDays(-50) }
            };
            var input = new RepoSyncRequest
            {
                AccountId = "851841f538f3e05cd437913851078076"
            };
            var response = await polling.OnRepoSyncFailure(request, input);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
