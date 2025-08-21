using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Project;
using Newtonsoft.Json;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class ProjectActionTests : TestBase
{
    [TestMethod]
    public async Task SearchProjects_IsSucssess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);

        var response = await actions.SearchProjects(new SearchProjectsRequest { });

        var json = JsonConvert.SerializeObject(response, Formatting.Indented);
        Console.WriteLine(json);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetProject_IsSucssess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);

        var response = await actions.GetProject(new ProjectRequest { ProjectId = "a53022230e25f47a7273c029a92de746" });

        var json = JsonConvert.SerializeObject(response, Formatting.Indented);
        Console.WriteLine(json);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task GetProjectBySlug_IsSuccess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);

        var response = await actions.GetProjectBySlug("demo-web-app");

        var json = JsonConvert.SerializeObject(response, Formatting.Indented);

        Console.WriteLine(json);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task DeleteProject_IsSucssess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        await actions.DeleteProject(new ProjectRequest { ProjectId = "d78549dad981cb310133ac81683d397d" });

        Assert.IsTrue(true);
    }

    [TestMethod]
    public async Task CreateProject_IsSucssess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        var result = await actions.CreateProject(new CreateProjectRequest { Name = "My New Project2", },
            new FileRequest { File=null });

        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(json);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateProject_IsSucssess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        var result = await actions.UpdateProject(new CreateProjectRequest { Name = "My New Project(New name after update)", },
            new FileRequest
            { }, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(json);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetProjectLocales_IsSuccess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        var result = await actions.GetProjectLocales(
            new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
            new GetProjectLocalesRequest { ReturnTargetLocalesOnly = true });
        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(json);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetProjectLocaleFromCode_IsSuccess()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        var result = await actions.GetProjectLocaleFromCode(
            new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" },
            new GetProjectLocaleFromCodeRequest { LocaleCode = "nl-NL" });

        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(json);
        Assert.IsNotNull(result);
    }
}
