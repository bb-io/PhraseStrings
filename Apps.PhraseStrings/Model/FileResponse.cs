using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.PhraseStrings.Model
{
    public class FileResponse
    {
        [Display("File")]
        public FileReference File { get; set; } = new();
    }
}
