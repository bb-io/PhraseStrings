using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key
{
    public class GetTranslationsForKeyRequest
    {
        [Display("Branch")]
        [JsonProperty("branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [Display("Sort")]
        [JsonProperty("sort")]
        public string? Sort { get; set; }

        [Display("Order")]
        [JsonProperty("order")]
        public string? Order { get; set; }

        [Display("Query")]
        [JsonProperty("q")]
        public string? Query { get; set; }
    }
}
