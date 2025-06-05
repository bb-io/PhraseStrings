using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Job
{
    public class JobRequest
    {
        [Display("Job ID")]
        [DataSource(typeof(JobDataHandler))]
        public string JobId { get; set; }

    }
}
