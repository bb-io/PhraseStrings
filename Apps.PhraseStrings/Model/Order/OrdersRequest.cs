using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Order
{
    public class OrdersRequest
    {
        [Display("Order ID")]
        [DataSource(typeof(OrderDataHandler))]
        public string OrderId { get; set; } = string.Empty;
    }
}
