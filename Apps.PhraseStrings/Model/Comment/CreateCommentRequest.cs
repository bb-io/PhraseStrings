using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Comment
{
    public class CreateCommentRequest
    {
        public string? Message { get; set; }

        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [Display("Locales")]
        public IEnumerable<string>? Locales { get; set; }

    }
}
