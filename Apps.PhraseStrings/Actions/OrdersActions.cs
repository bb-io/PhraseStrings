using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.Model.Order;
using Apps.PhraseStrings.Model.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.PhraseStrings.Actions
{
    [ActionList("Orders")]
    public class OrdersActions(InvocationContext invocationContext) : PhraseStringsInvocable(invocationContext)
    {
        [Action("Search orders", Description = "Searches orders")]
        public async Task<ListOrdersResponse> SearchOrders([ActionParameter] SearchOrdersRequest input,
            [ActionParameter] ProjectRequest project)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/orders", Method.Get);

            if (!string.IsNullOrEmpty(input.Branch))
            {
                request.AddQueryParameter("branch", input.Branch);
            }
            
            var orders = await Client.Paginate<OrderResponse>(request);
            return new ListOrdersResponse { Orders = orders };

        }


        [Action("Create order", Description = "Creates order")]
        public async Task<CreateOrderResponse> CreateOrder([ActionParameter] CreateOrderRequest input,
            [ActionParameter] ProjectRequest project)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/orders", Method.Post);

            var body = new Dictionary<string, object>
            {
                ["name"] = input.Name,
                ["lsp"] = input.Lsp
            };

            if (!string.IsNullOrEmpty(input.Branch))
                body["branch"] = input.Branch;

            if (!string.IsNullOrEmpty(input.Category))
                body["category"] = input.Category;

            if (!string.IsNullOrEmpty(input.SourceLocaleId))
                body["source_locale_id"] = input.SourceLocaleId;

            if (input.TargetLocaleIds != null && input.TargetLocaleIds.Count > 0)
                body["target_locale_ids"] = input.TargetLocaleIds;

            if (!string.IsNullOrEmpty(input.TranslationType))
                body["translation_type"] = input.TranslationType;

            if (!string.IsNullOrEmpty(input.Tag))
                body["tag"] = input.Tag;

            if (!string.IsNullOrEmpty(input.Message))
                body["message"] = input.Message;

            if (!string.IsNullOrEmpty(input.StyleguideId))
                body["styleguide_id"] = input.StyleguideId;

            if (input.UnverifyTranslationsUponDelivery.HasValue)
                body["unverify_translations_upon_delivery"] = input.UnverifyTranslationsUponDelivery.Value;

            if (input.IncludeUntranslatedKeys.HasValue)
                body["include_untranslated_keys"] = input.IncludeUntranslatedKeys.Value;

            if (input.IncludeUnverifiedTranslations.HasValue)
                body["include_unverified_translations"] = input.IncludeUnverifiedTranslations.Value;

            if (input.Quality.HasValue)
                body["quality"] = input.Quality.Value;

            if (input.Priority.HasValue)
                body["priority"] = input.Priority.Value;

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            return await Client.ExecuteWithErrorHandling<CreateOrderResponse>(request);            
        }


        [Action("Get order", Description = "Gets order")]
        public async Task<OrderResponse> GetOrder([ActionParameter] OrdersRequest input,
           [ActionParameter] ProjectRequest project,
           [ActionParameter]  [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]string? Branch )
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/orders", Method.Get);

            if (!string.IsNullOrEmpty(Branch))
            {
                request.AddQueryParameter("branch", Branch);
            }

            return await Client.ExecuteWithErrorHandling<OrderResponse>(request);

        }

        [Action("Confirm order", Description = "Confirms order")]
        public async Task<OrderResponse> ConfirmOrder([ActionParameter] OrdersRequest order,
           [ActionParameter] ProjectRequest project)
        {
            var request = new RestRequest($"/v2/projects/{project.ProjectId}/orders/{order.OrderId}/confirm", Method.Get);
            return await Client.ExecuteWithErrorHandling<OrderResponse>(request);
        }
    }
}
