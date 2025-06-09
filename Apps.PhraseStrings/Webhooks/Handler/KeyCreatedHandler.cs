using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Base;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.PhraseStrings.Webhooks.Handler
{
    public class KeyCreatedHandler : PhraseStringsWebhookHandler
    {
        protected override string SubscriptionEvents => "keys:create";
        public KeyCreatedHandler(InvocationContext invocationContext, [WebhookParameter(true)] ProjectRequest input) : base(invocationContext, input)
        {
        }
    }
}
