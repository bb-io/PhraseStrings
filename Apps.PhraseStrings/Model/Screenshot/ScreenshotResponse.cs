using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Screenshot
{
    public class ScreenshotResponse
    {
        [JsonProperty("id")]
        [Display("Screenshot ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        [Display("Screenshot name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        [Display("Screenshot description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("screenshot_url")]
        [Display("Screenshot URL")]
        public Uri? ScreenshotUrl { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("markers_count")]
        [Display("Markers count")]
        public int MarkersCount { get; set; }
    }
}
