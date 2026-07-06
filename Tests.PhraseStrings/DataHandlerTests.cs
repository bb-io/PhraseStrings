using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.Handlers;
using Apps.PhraseStrings.Model.Account;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings;

[TestClass]
public class DataHandlerTests : TestBaseMultipleConnections
{
    [TestMethod, ContextDataSource]
    public async Task ProjectHandler_IsSuccess(InvocationContext context)
    {
        var handler = new ProjectDataHandler(context);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task JobsHandler_IsSuccess(InvocationContext context)
    {
        var handler = new JobDataHandler(context, new ProjectRequest { ProjectId= "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);
        
        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task KeyHandler_IsSuccess(InvocationContext context)
    {
        var handler = new KeyDataHandler(context, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task VariableHandler_IsSuccess(InvocationContext context)
    {
        var handler = new VariableDataHandler(context, new ProjectRequest { ProjectId = "c8f4df95eb1eff7346b5d50e53e4918e" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, ContextDataSource]
    public async Task BranchHandler_IsSuccess(InvocationContext context)
    {
        var handler = new BranchDataHandler(context, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task OrderHandler_IsSuccess(InvocationContext context)
    {
        var handler = new OrderDataHandler(context, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task FormatHandler_IsSuccess(InvocationContext context)
    {
        var handler = new FormatDataHandler(context);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task TranslationHandler_IsSuccess(InvocationContext context)
    {
        var handler = new TranslationDataHandler(context, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task LocaleHandler_IsSuccess(InvocationContext context)
    {
        var handler = new LocaleDataHandler(context, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task AccountHandler_IsSuccess(InvocationContext context)
    {
        var handler = new AccountDataHandler(context);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task RepositoryHandler_IsSuccess(InvocationContext context)
    {
        var handler = new RepositoryDataHandler(context, new AccountRequest { AccountId = "851841f538f3e05cd437913851078076" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task ScreenshotHandler_IsSuccess(InvocationContext context)
    {
        var handler = new ScreenshotDataHandler(context, new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task TeamDataHandler_IsSuccess(InvocationContext context)
    {
        var handler = new TeamDataHandler(context, new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod, ContextDataSource]
    public async Task UserDataHandler_IsSuccess(InvocationContext context)
    {
        var handler = new UserDataHandler(context, new ProjectRequest { ProjectId = "d562a2ad202e4ab626b0764576660917" });

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        TestContext.WriteLine($"Total: {result.Count()}");
        PrintDataHandlerResult(result);
        Assert.IsTrue(result.Count() > 0);
    }
}
