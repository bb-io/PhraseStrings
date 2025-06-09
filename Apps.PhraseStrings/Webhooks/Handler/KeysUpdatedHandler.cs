using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Base;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.PhraseStrings.Webhooks.Handler
{
    public class KeysUpdatedHandler : PhraseStringsWebhookHandler
    {
        protected override string SubscriptionEvents => "keys:update";
        public KeysUpdatedHandler(InvocationContext invocationContext, [WebhookParameter(true)] ProjectRequest input) : base(invocationContext, input)
        {
        }
    }
}