using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class RepoSyncRequest
    {
        [Display("Account ID")]
        [DataSource(typeof(AccountDataHandler))]
        public string AccountId { get; set; } = string.Empty;
    }
}
