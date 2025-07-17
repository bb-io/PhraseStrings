using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class RepoSyncHistoryDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("auto_import")]
        public bool AutoImport { get; set; }

        [JsonProperty("errors")]
        public List<JToken> Errors { get; set; }
    }

    public class RepoSyncErrorBatch
    {
        public List<RepoSyncError> Errors { get; }

        public RepoSyncErrorBatch(List<RepoSyncError> errors)
        {
            Errors = errors;

        }
    }

    public class RepoSyncError
    {
        [Display("Sync ID")]
        public string SyncId { get; set; }

        [Display("Repository name")]
        public string RepoName { get; set; }


        public string ProjectId { get; set; }

        [Display("Project name")]
        public string ProjectName { get; set; }

        public string Provider { get; set; }

        [Display("Event type")]
        public string EventType { get; set; }

        [Display("Date")]
        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        [Display("Last import at")]
        public DateTime? LastImportAt { get; set; }

        [Display("Last export at")]
        public DateTime? LastExportAt { get; set; }

        [Display("Event status")]
        public string EventStatus { get; set; }
    }
}
