using Apps.PhraseStrings.Model.Job;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Repository;

public class RepositoryResponse
{
    [Display("Repository ID")]
    [JsonProperty("id")]
    public string RepoId { get; set; } = string.Empty;

    [Display("Repository name")]
    [JsonProperty("repo_name")]
    public string RepoName { get; set; } = string.Empty;

    [Display("Provider")]
    [JsonProperty("provider")]
    public string Provider { get; set; } = string.Empty;

    [Display("Is sync enabled?")]
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

    [Display("Is auto-imporn enabled?")]
    [JsonProperty("auto_import")]
    public bool AutoImport { get; set; }

    [Display("Sync created at")]
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [Display("Last imported at")]
    [JsonProperty("last_import_at")]
    public DateTime? LastImportAt { get; set; }

    [Display("Last exported at")]
    [JsonProperty("last_export_at")]
    public DateTime? LastExportAt { get; set; }

    [Display("Project")]
    [JsonProperty("project")]
    public ProjectInfo Project { get; set; } = new();
}
