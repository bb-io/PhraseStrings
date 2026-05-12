using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Order;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class OrderActionTests : TestBaseMultipleConnections
    {
        [TestMethod, ContextDataSource]
        public async Task SearchOrders_IssSuccess(InvocationContext context)
        {
            var actions = new OrdersActions(context);
            var response = await actions.SearchOrders(
                new SearchOrdersRequest { },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }

        [TestMethod, ContextDataSource]
        public async Task CreateOrder_IssSuccess(InvocationContext context)
        {
            var actions = new OrdersActions(context);
            var response = await actions.CreateOrder(
                new CreateOrderRequest
                {
                    Name = "Test Order from locale",
                    Lsp = "textmaster",
                    TranslationType = "enterprise",
                    TargetLocaleIds = new List<string> { "es" },
                    Category = "internet",

                },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

            PrintResult(response);
            Assert.IsNotNull(response);
        }
    }
}
