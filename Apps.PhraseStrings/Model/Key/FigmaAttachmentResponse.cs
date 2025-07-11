using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class FigmaAttachmentResponse
    {
        [JsonProperty("id")]
        [Display("Attachment ID")]
        public string Id { get; set; }

        [JsonProperty("url")]
        [Display("Figma URL")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }
    }
}
