using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model
{
    public class FileFormatResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("api_name")]
        public string ApiName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("default_encoding")]
        public string DefaultEncoding { get; set; }

        [JsonProperty("importable")]
        public bool Importable { get; set; }

        [JsonProperty("exportable")]
        public bool Exportable { get; set; }

        [JsonProperty("default_file")]
        public string DefaultFile { get; set; }

        [JsonProperty("renders_default_locale")]
        public bool RendersDefaultLocale { get; set; }

        [JsonProperty("includes_locale_information")]
        public bool IncludesLocaleInformation { get; set; }
    }
}
