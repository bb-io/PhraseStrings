using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Key
{
    public class UploadFigmaLinkRequest
    {
        [Display("URL")]
        public string Url { get; set; } = string.Empty;

        [Display("Key ID")]
        [DataSource(typeof(KeyDataHandler))]
        public string KeyId { get; set; } = string.Empty;

        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }
    }
}
