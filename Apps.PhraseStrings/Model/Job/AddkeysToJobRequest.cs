using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class AddkeysToJobRequest
    {
        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [JsonProperty("translation_key_ids")]
        [DataSource(typeof(KeyDataHandler))]
        public List<string>? Keys { get; set; }
    }
}
