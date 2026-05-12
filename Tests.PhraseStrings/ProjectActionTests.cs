using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class ProjectActionTests : TestBaseMultipleConnections
{
    [TestMethod, ContextDataSource]
    public async Task SearchProjects_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);

        var response = await actions.SearchProjects(new SearchProjectsRequest { });

        PrintResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod, ContextDataSource]
    public async Task GetProject_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);

        var response = await actions.GetProject(new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" });

        PrintResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod, ContextDataSource]
    public async Task GetProjectBySlug_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);

        var response = await actions.GetProjectBySlug("demo-web-app");

        PrintResult(response);
        Assert.IsNotNull(response);
    }

    [TestMethod, ContextDataSource]
    public async Task DeleteProject_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);
        await actions.DeleteProject(new ProjectRequest { ProjectId = "d78549dad981cb310133ac81683d397d" });

        Assert.IsTrue(true);
    }

    [TestMethod, ContextDataSource]
    public async Task CreateProject_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);
        var result = await actions.CreateProject(new CreateProjectRequest { Name = "My New Project2", },
            new FileRequest { File=null });

        PrintResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, ContextDataSource]
    public async Task UpdateProject_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);
        var result = await actions.UpdateProject(new CreateProjectRequest { Name = "My New Project(New name after update)", },
            new FileRequest
            { }, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        PrintResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, ContextDataSource]
    public async Task GetProjectLocales_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);
        var result = await actions.GetProjectLocales(
            new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
            new GetProjectLocalesRequest { ReturnTargetLocalesOnly = true });
        
        PrintResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, ContextDataSource]
    public async Task GetProjectLocaleFromCode_IsSuccess(InvocationContext context)
    {
        var actions = new ProjectActions(context, FileManager);
        var result = await actions.GetProjectLocaleFromCode(
            new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
            new GetProjectLocaleFromCodeRequest { LocaleCode = "nl-NL" });

        PrintResult(result);
        Assert.IsNotNull(result);
    }
}
