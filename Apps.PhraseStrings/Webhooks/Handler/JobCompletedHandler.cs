using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Webhooks.Base;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.PhraseStrings.Webhooks.Handler
{
    public class JobCompletedHandler : PhraseStringsWebhookHandler
    {
        protected override string SubscriptionEvents => "jobs:complete";

        public JobCompletedHandler(InvocationContext invocationContext, [WebhookParameter(true)] ProjectRequest input) : base(invocationContext, input)
        {
        }
    }
}
