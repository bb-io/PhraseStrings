using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class AddkeysToJobRequest
    {
        [JsonProperty("branch")]
        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("translation_key_ids")]
        [Display("Key IDs")]
        [DataSource(typeof(KeyDataHandler))]
        public List<string>? Keys { get; set; }

        [Display("Key names")]
        public List<string>? KeyNames { get; set; }
    }
}
