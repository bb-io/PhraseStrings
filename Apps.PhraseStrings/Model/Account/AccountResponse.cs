using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Account
{
    public class AccountResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty("company")]
        public string Company { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("company_logo_url")]
        public string CompanyLogoUrl { get; set; } = string.Empty;
    }
}
