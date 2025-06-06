using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Account
{
    public class AccountResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("company_logo_url")]
        public string CompanyLogoUrl { get; set; }
    }
}
