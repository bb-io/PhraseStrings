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
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

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
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class Account
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
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("company_logo_url")]
        [Display("Company logo URL")]
        public string CompanyLogoUrl { get; set; } = string.Empty;
    }
}
