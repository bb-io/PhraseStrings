using Apps.PhraseStrings.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.PhraseStrings;

public class PhraseStringsInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected PhraseStringsClient Client { get; }
    public PhraseStringsInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }
}