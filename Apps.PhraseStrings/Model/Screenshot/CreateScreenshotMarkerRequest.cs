using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Screenshot
{
    public class CreateScreenshotMarkerRequest
    {
        [Display("Key ID")]
        [DataSource(typeof(KeyDataHandler))]
        [JsonProperty("key_id")]
        public string KeyId { get; set; } = string.Empty;

        [Display("Screenshot ID")]
        [JsonProperty("screenshot_id")]
        [DataSource(typeof(ScreenshotDataHandler))]
        public string ScreenshotId { get; set; } = string.Empty;

        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        [JsonProperty("branch")]
        public string? Branch { get; set; }

        [Display("Presentation details")]
        [JsonProperty("presentation")]
        public string? Presentation { get; set; }
    }
}
