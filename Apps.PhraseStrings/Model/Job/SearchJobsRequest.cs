using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.DataHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Job
{
    public class SearchJobsRequest
    {
        [Display("Branch")]
        [DataSource(typeof(BranchDataHandler))]
        public string? Branch { get; set; }

        [Display("Owned by")]
        public string? OwnedBy { get; set; }

        [Display("Assigned to")]
        public string? AssignedTo { get; set; }

        [Display("State")]
        [StaticDataSource(typeof(StateDataHandler))]
        public string? State { get; set; }

        [Display("Updated since")]
        public DateTime? UpdatedSince { get; set; }
    }
}
