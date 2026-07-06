using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Api;
using Apps.PhraseStrings.Constants;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Variable;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class ProjectVariableActionTests : TestBaseMultipleConnections
{
    private const string ExistingVariableName = "BB_VAR";
    private const string ExistingVariableValue = "Initial value";
    private const string NewVariableName = "BB_NEW";

    private InvocationContext _context = null!;
    private PhraseStringsClient _client = null!;
    private ProjectVariablesActions _variableActions = null!;
    private ProjectResponse? _project;

    [TestInitialize]
    public async Task CreateProjectWithVariable()
    {
        _context = GetInvocationContext("Access token");
        await AssertApiHostReachable(_context);

        _client = new PhraseStringsClient(_context.AuthenticationCredentialsProviders);
        _variableActions = new ProjectVariablesActions(_context);
        var projectActions = new ProjectActions(_context, FileManager);

        try
        {
            _project = await projectActions.CreateProject(
                new CreateProjectRequest
                {
                    Name = $"Blackbird project variables {Guid.NewGuid():N}",
                    MainFormat = "json"
                },
                new FileRequest());

            await CreateVariable(_client, _project.Id, ExistingVariableName, ExistingVariableValue);
        }
        catch
        {
            if (_project is not null)
                await DeleteProject(_client, _project.Id);

            _project = null;
            throw;
        }
    }

    [TestCleanup]
    public async Task DeleteProject()
    {
        if (_project is not null)
            await DeleteProject(_client, _project.Id);
    }

    [TestMethod]
    public async Task GetVariableValue_ExistingVariable_IsSuccess()
    {
        var response = await _variableActions.GetVariableValue(
            ProjectInput(),
            new VariableRequest { Name = ExistingVariableName });

        Assert.AreEqual(ExistingVariableName, response.Name);
        Assert.AreEqual(ExistingVariableValue, response.Value);
        Assert.IsNotNull(response.CreatedAt);
        Assert.IsNotNull(response.UpdatedAt);
    }

    [TestMethod]
    public async Task SetVariable_ExistingVariable_IsSuccess()
    {
        const string updatedValue = "Updated value";

        var response = await _variableActions.SetVariable(
            ProjectInput(),
            new VariableRequest { Name = ExistingVariableName },
            new VariableValueRequest { Value = updatedValue });

        Assert.AreEqual(ExistingVariableName, response.Name);
        Assert.AreEqual(updatedValue, response.Value);
        Assert.IsNotNull(response.CreatedAt);
        Assert.IsNotNull(response.UpdatedAt);

        var getUpdatedResponse = await _variableActions.GetVariableValue(
            ProjectInput(),
            new VariableRequest { Name = ExistingVariableName });

        Assert.AreEqual(updatedValue, getUpdatedResponse.Value);
    }

    [TestMethod]
    public async Task SetVariable_NewVariable_IsSuccess()
    {
        const string newValue = "New value";

        var response = await _variableActions.SetVariable(
            ProjectInput(),
            new VariableRequest { Name = NewVariableName },
            new VariableValueRequest { Value = newValue });

        Assert.AreEqual(NewVariableName, response.Name);
        Assert.AreEqual(newValue, response.Value);
        Assert.IsNotNull(response.CreatedAt);
        Assert.IsNotNull(response.UpdatedAt);

        var getNewResponse = await _variableActions.GetVariableValue(
            ProjectInput(),
            new VariableRequest { Name = NewVariableName });

        Assert.AreEqual(newValue, getNewResponse.Value);
    }

    private ProjectRequest ProjectInput()
        => new() { ProjectId = _project?.Id ?? throw new InvalidOperationException("Test project was not created.") };

    private static async Task<VariableResponse> CreateVariable(
        PhraseStringsClient client,
        string projectId,
        string name,
        string value)
    {
        var request = new RestRequest("/v2/projects/{projectId}/variables", Method.Post)
            .AddUrlSegment("projectId", projectId)
            .AddJsonBody(new Dictionary<string, object>
            {
                ["name"] = name,
                ["value"] = value
            });

        return await client.ExecuteWithErrorHandling<VariableResponse>(request);
    }

    private static async Task DeleteProject(PhraseStringsClient client, string projectId)
    {
        var request = new RestRequest($"/v2/projects/{projectId}", Method.Delete);
        await client.ExecuteWithErrorHandling(request);
    }

    private static async Task AssertApiHostReachable(InvocationContext context)
    {
        var url = context.AuthenticationCredentialsProviders.First(provider => provider.KeyName == CredsNames.Url).Value;
        var host = new Uri(url).Host;

        try
        {
            await System.Net.Dns.GetHostAddressesAsync(host);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Phrase Strings API host is not reachable from this environment: {ex.Message}");
        }
    }
}
