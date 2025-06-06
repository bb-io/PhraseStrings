using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.Handlers;
using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class DataHandlerTests : TestBase
{
    [TestMethod]
    public async Task ProjectHandler_IsSuccess()
    {
        var handler = new ProjectDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task JobsHandler_IsSuccess()
    {
        var handler = new JobDataHandler(InvocationContext, new ProjectRequest { ProjectId= "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task KeyHandler_IsSuccess()
    {
        var handler = new KeyDataHandler(InvocationContext, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task BranchHandler_IsSuccess()
    {
        var handler = new BranchDataHandler(InvocationContext, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task OrderHandler_IsSuccess()
    {
        var handler = new OrderDataHandler(InvocationContext, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }


    [TestMethod]
    public async Task FormatHandler_IsSuccess()
    {
        var handler = new FormatDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task TranslationHandler_IsSuccess()
    {
        var handler = new TranslationDataHandler(InvocationContext, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task LocaleHandler_IsSuccess()
    {
        var handler = new LocaleDataHandler(InvocationContext, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task AccountHandler_IsSuccess()
    {
        var handler = new AccountDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task RepositoryHandler_IsSuccess()
    {
        var handler = new RepositoryDataHandler(InvocationContext, new AccountRequest { AccoutnId = "851841f538f3e05cd437913851078076" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsNotNull(result);
    }
}
