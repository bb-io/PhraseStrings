using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Order;
using Apps.PhraseStrings.Model.Project;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class OrderActionTests : TestBase
    {
        [TestMethod]
        public async Task SearchOrders_IssSuccess()
        {
            var action = new OrdersActions(InvocationContext, FileManager);
            var response = await action.SearchOrders(
                new SearchOrdersRequest
                {
                },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });
            Console.WriteLine($"Total: {response.Orders.Count}");
            foreach (var order in response.Orders)
            {
                Console.WriteLine($"{order.Id}: {order.State}");
            }
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreateOrder_IssSuccess()
        {
            var action = new OrdersActions(InvocationContext, FileManager);
            var response = await action.CreateOrder(
                new CreateOrderRequest
                {
                    Name = "Test Order from locale",
                    Lsp = "textmaster",
                    TranslationType = "enterprise",
                    TargetLocaleIds = new List<string> { "es" },
                    Category = "internet",

                },
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" });

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
