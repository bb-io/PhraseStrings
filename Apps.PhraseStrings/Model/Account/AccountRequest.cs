using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Account
{
    public class AccountRequest
    {
        [Display("Account ID")]
        [DataSource(typeof(AccountDataHandler))]
        public string AccountId { get; set; } = string.Empty;
    }
}
