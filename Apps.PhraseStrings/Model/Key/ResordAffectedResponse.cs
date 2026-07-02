using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class ResordAffectedResponse
    {
        [JsonProperty("records_affected")]
        [Display("Records affected")]
        public string? RecordAffected { get; set; }
    }
}
