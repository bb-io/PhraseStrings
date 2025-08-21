using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Project
{
    public class ListProjectsResponse
    {
        public List<ProjectResponse> Projects { get; set; } = [];
    }

    public class ProjectResponse
    {
        [Display("Project ID")]
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [Display("Project name")]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [Display("Project slug")]
        [JsonProperty("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty("main_format")]
        [Display("Main format")]
        public string MainFormat { get; set; } = string.Empty;

        [JsonProperty("project_image_url")]
        [Display("Project image URL")]
        public string ProjectImageUrl { get; set; } = string.Empty;

        [JsonProperty("account")]
        public Account Account { get; set; } = new();

        [JsonProperty("space")]
        public object Space { get; set; } = new();

        [JsonProperty("point_of_contact")]
        [Display("Point of contact")]
        public object PointOfContact { get; set; } = new();

        [JsonProperty("created_at")]
        [Display("Project created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Project last updated at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class Account
    {
        [Display("Account ID")]
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [Display("Account name")]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [Display("Account slug")]
        [JsonProperty("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty("company")]
        public string Company { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        [Display("Account created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Account last updated at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("company_logo_url")]
        [Display("Company logo URL")]
        public string CompanyLogoUrl { get; set; } = string.Empty;
    }
}
