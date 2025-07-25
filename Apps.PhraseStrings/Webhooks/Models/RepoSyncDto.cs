using Newtonsoft.Json;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class RepoSyncDto
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("project")]
        public ProjectDto Project { get; set; } = new ProjectDto();

        [JsonProperty("provider")]
        public string Provider { get; set; } = string.Empty;

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("auto_import")]
        public bool AutoImport { get; set; }

        [JsonProperty("repo_name")]
        public string RepoName { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("last_import_at")]
        public DateTime? LastImportAt { get; set; }

        [JsonProperty("last_export_at")]
        public DateTime? LastExportAt { get; set; }
    }

    public class ProjectDto
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("main_format")]
        public string MainFormat { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

}
