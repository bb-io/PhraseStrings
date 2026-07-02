using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Project
{
    public class ListProjectsResponse
    {
        [Display("Projects")]
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
        [Display("Account")]
        public Account Account { get; set; } = new();

        [JsonProperty("space")]
        [Display("Space")]
        public ProjectSpace? Space { get; set; }

        [JsonProperty("point_of_contact")]
        [Display("Point of contact")]
        public ProjectUser? PointOfContact { get; set; }

        [JsonProperty("created_at")]
        [Display("Project created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Project last updated at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class ProjectSpace
    {
        [Display("Space ID")]
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [Display("Space name")]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [Display("Space created at")]
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Display("Space updated at")]
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Display("Projects count")]
        [JsonProperty("projects_count")]
        public int? ProjectsCount { get; set; }
    }

    public class ProjectUser
    {
        [Display("User ID")]
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [Display("Username")]
        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [Display("Name")]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [Display("Gravatar UID")]
        [JsonProperty("gravatar_uid")]
        public string GravatarUid { get; set; } = string.Empty;

        [Display("Email")]
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
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
        [Display("Company")]
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
