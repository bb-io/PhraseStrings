using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Key
{
    public class SearchKeysRequest
    {
        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [Display("Sort")]
        public string? Sort { get; set; }

        [Display("Order")]
        public string? Order { get; set; }

        [Display("Locale ID")]
        public string? LocaleId { get; set; }

        [Display("Tags")]
        public IEnumerable<string>? Tags { get; set; }
    }
}
