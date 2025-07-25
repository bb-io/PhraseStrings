using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Screenshot
{
    public class ScreenshotDtoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("screenshot_url")]
        public Uri? ScreenshotUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("markers_count")]
        public int MarkersCount { get; set; }
    }
}
