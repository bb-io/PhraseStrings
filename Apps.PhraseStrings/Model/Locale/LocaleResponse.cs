using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.PhraseStrings.Model.Locale
{
    public class LocaleResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("default")]
        public bool IsDefault { get; set; }

        [JsonProperty("main")]
        public bool IsMain { get; set; }

        [JsonProperty("rtl")]
        public bool IsRtl { get; set; }

        [JsonProperty("plural_forms")]
        public List<string> PluralForms { get; set; }

        [JsonProperty("source_locale")]
        public LocaleReference SourceLocale { get; set; }

        [JsonProperty("fallback_locale")]
        public LocaleReference FallbackLocale { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class LocaleReference
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
