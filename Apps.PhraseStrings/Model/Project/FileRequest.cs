using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.PhraseStrings.Model.Project
{
    public class FileRequest
    {
        [Display("File")]
        public FileReference? File { get; set; }
    }
}
