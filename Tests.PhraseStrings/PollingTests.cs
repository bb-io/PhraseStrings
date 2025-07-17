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
                //Memory = new DateMemory { LastInteractionDate = DateTime.UtcNow.AddDays(-20) }
            };
            var input = new RepoSyncRequest
            {
                AccountId = "dd76e8ff50005091bd5c1757c2b6e893"
            };
            var response = await polling.OnRepoSyncFailure(request, input);

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
