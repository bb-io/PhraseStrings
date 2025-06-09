using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Project
{
    public class ListProjectsResponse
    {
        public List<ProjectResponse> Projects { get; set; }
    }

    public class ProjectResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("main_format")]
        [Display("Main format")]
        public string MainFormat { get; set; }

        [JsonProperty("project_image_url")]
        [Display("Project image URL")]
        public string ProjectImageUrl { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("space")]
        public object Space { get; set; }

        [JsonProperty("point_of_contact")]
        [Display("Point of contact")]
        public object PointOfContact { get; set; }

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
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("company_logo_url")]
        [Display("Company logo URL")]
        public string CompanyLogoUrl { get; set; }
    }
}
