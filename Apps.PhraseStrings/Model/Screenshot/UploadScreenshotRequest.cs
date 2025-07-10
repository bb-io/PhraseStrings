using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Screenshot
{
    public class UploadScreenshotRequest
    {
        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        [JsonProperty("branch")]
        public string? Branch { get; set; }

        [Display("Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Display("Description")]
        [JsonProperty("description")]
        public string? Description { get; set; }

        [Display("Screenshot")]
        [JsonProperty("filename")]
        public FileReference File { get; set; }
    }
}
