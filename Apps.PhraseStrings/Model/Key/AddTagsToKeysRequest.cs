using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class AddTagsToKeysRequest
    {
        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("locale_id")]
        [Display("Source locale ID")]
        public string? LocaleId { get; set; }

        [JsonProperty("tags")]
        [Display("Tags (comma-separated)")]
        public string? Tags { get; set; }


        public string? Query { get; set; }
    }
}
