using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Repository
{
    public class ImportResponse
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("auto_import")]
        [Display("Auto import")]
        public bool? AutoImport { get; set; }

        [JsonProperty("errors")]
        public List<string>? Errors { get; set; }
    }
}
