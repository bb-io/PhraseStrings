using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class ResordAffectedResponse
    {
        [JsonProperty("records_affected")]
        public string? RecordAffected { get; set; }
    }
}
