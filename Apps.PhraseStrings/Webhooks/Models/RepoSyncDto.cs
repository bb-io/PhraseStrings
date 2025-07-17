using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class RepoSyncDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project")]
        public ProjectDto Project { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("auto_import")]
        public bool AutoImport { get; set; }

        [JsonProperty("repo_name")]
        public string RepoName { get; set; }

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
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("main_format")]
        public string MainFormat { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

}
