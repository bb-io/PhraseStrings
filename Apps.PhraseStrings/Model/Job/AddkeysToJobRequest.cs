using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Job
{
    public class AddkeysToJobRequest
    {
        [JsonProperty("branch")]
        public string? Branch { get; set; }

        [JsonProperty("translation_key_ids")]
        public List<string>? Keys { get; set; }
    }
}
