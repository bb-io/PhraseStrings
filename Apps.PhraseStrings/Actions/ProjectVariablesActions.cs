using System.Net;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Variable;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.PhraseStrings.Actions;

[ActionList("Project variables")]
public class ProjectVariablesActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
{
    [Action("Get variable value", Description = "Get a project variable value.")]
    public async Task<VariableResponse> GetVariableValue(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] VariableRequest variable)
    {
        var request = new RestRequest("/v2/projects/{projectId}/variables/{name}", Method.Get)
            .AddUrlSegment("projectId", project.ProjectId)
            .AddUrlSegment("name", variable.Name);

        return await Client.ExecuteWithErrorHandling<VariableResponse>(request);
    }

    [Action("Set variable", Description = "Create or update a project variable.")]
    public async Task<VariableResponse> SetVariable(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] VariableRequest variable,
        [ActionParameter] VariableValueRequest input)
    {
        var body = new Dictionary<string, object>
        {
            ["name"] = variable.Name,
            ["value"] = input.Value
        };

        var updateRequest = new RestRequest("/v2/projects/{projectId}/variables/{name}", Method.Patch)
            .AddUrlSegment("projectId", project.ProjectId)
            .AddUrlSegment("name", variable.Name)
            .AddJsonBody(body);

        var updateResponse = await Client.ExecuteAsync(updateRequest);
        if (updateResponse.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<VariableResponse>(updateResponse.Content ?? string.Empty)
                   ?? throw new PluginApplicationException($"Could not parse variable response: {updateResponse.Content}");
        }

        if (updateResponse.StatusCode != HttpStatusCode.NotFound)
            throw new PluginApplicationException(updateResponse.Content ?? updateResponse.StatusDescription ?? "Error when running a request");

        var createRequest = new RestRequest("/v2/projects/{projectId}/variables", Method.Post)
            .AddUrlSegment("projectId", project.ProjectId)
            .AddJsonBody(body);

        return await Client.ExecuteWithErrorHandling<VariableResponse>(createRequest);
    }
}
