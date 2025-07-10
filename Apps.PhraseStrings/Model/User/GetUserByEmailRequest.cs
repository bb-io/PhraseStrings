using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.User;

public class GetUserByEmailRequest
{
    [Display("Account ID")]
    [DataSource(typeof(AccountDataHandler))]
    public string AccountId { get; set; } = string.Empty;

    [Display("Email")]
    public string Email { get; set; } = string.Empty;
}
