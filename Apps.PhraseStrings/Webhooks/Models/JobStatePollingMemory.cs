using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Webhooks.Models
{
    public class JobStatePollingMemory
    {
        [Display("Last polling time")]
        public DateTime LastPollingTime { get; set; }

        [Display("Last job state")]
        public string? LastState { get; set; }
    }
}
