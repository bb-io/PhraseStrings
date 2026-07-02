using Apps.PhraseStrings.Webhooks;
using Apps.PhraseStrings.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json.Linq;

namespace Tests.PhraseStrings;

[TestClass]
public class WebhookTests
{
    [DataTestMethod]
    [DataRow("phrase-job-created-webhook.json", "jobs:create")]
    [DataRow("phrase-job-activated-webhook.json", "jobs:start")]
    [DataRow("phrase-job-completed-webhook.json", "jobs:complete")]
    public async Task OnPhraseJobStatusChange_Flies_ForSelectedEventAndProject(string fileName, string eventName)
    {
        var response = await Invoke(fileName, new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = [eventName],
            ProjectIds = [GetProjectId(fileName)]
        });

        Assert.AreEqual(WebhookRequestType.Default, response.ReceivedWebhookRequestType);
        Assert.AreEqual(eventName, response.Result?.Event);
    }

    [TestMethod]
    public async Task OnPhraseJobStatusChange_Preflight_WhenEventNotSelected()
    {
        var response = await Invoke("phrase-job-created-webhook.json", new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = ["jobs:complete"],
            ProjectIds = [GetProjectId("phrase-job-created-webhook.json")]
        });

        Assert.AreEqual(WebhookRequestType.Preflight, response.ReceivedWebhookRequestType);
    }

    [TestMethod]
    public async Task OnPhraseJobStatusChange_Preflight_WhenProjectNotSelected()
    {
        var response = await Invoke("phrase-job-created-webhook.json", new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = ["jobs:create"],
            ProjectIds = ["different-project"]
        });

        Assert.AreEqual(WebhookRequestType.Preflight, response.ReceivedWebhookRequestType);
    }

    [TestMethod]
    public async Task OnPhraseJobStatusChange_FiltersByJobIds()
    {
        var matching = await Invoke("phrase-job-created-webhook.json", new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = ["jobs:create"],
            ProjectIds = [GetProjectId("phrase-job-created-webhook.json")],
            JobIds = [GetJobId("phrase-job-created-webhook.json")]
        });

        var nonMatching = await Invoke("phrase-job-created-webhook.json", new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = ["jobs:create"],
            ProjectIds = [GetProjectId("phrase-job-created-webhook.json")],
            JobIds = ["different-job"]
        });

        Assert.AreEqual(WebhookRequestType.Default, matching.ReceivedWebhookRequestType);
        Assert.AreEqual(WebhookRequestType.Preflight, nonMatching.ReceivedWebhookRequestType);
    }

    [TestMethod]
    public async Task OnPhraseJobStatusChange_FiltersByJobNameContains_CaseInsensitive()
    {
        var response = await Invoke("phrase-job-created-webhook.json", new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = ["jobs:create"],
            ProjectIds = [GetProjectId("phrase-job-created-webhook.json")],
            JobNameContains = "case test"
        });

        Assert.AreEqual(WebhookRequestType.Default, response.ReceivedWebhookRequestType);
    }

    [TestMethod]
    public async Task OnPhraseJobStatusChange_FiltersByJobNameDoesntContain_CaseInsensitive()
    {
        var response = await Invoke("phrase-job-created-webhook.json", new PhraseJobStatusChangeRequest
        {
            EventsToReactTo = ["jobs:create"],
            ProjectIds = [GetProjectId("phrase-job-created-webhook.json")],
            JobNameDoesntContain = "case test"
        });

        Assert.AreEqual(WebhookRequestType.Preflight, response.ReceivedWebhookRequestType);
    }

    private static Task<WebhookResponse<PhraseJobStatusChangedWebhookResponse>> Invoke(
        string fileName,
        PhraseJobStatusChangeRequest input)
    {
        var webhookList = new WebhookList(new InvocationContext());
        var request = new WebhookRequest
        {
            Body = ReadFixture(fileName)
        };

        return webhookList.OnPhraseJobStatusChange(request, input);
    }

    private static string GetProjectId(string fileName)
        => JObject.Parse(ReadFixture(fileName))["project"]?["id"]?.Value<string>()
           ?? throw new InvalidOperationException("Fixture project ID is missing.");

    private static string GetJobId(string fileName)
        => JObject.Parse(ReadFixture(fileName))["job"]?["id"]?.Value<string>()
           ?? throw new InvalidOperationException("Fixture job ID is missing.");

    private static string ReadFixture(string fileName)
        => File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestFiles", "Input", fileName));
}
