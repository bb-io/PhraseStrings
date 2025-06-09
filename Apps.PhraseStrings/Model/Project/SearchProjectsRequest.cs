using Apps.PhraseStrings.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.PhraseStrings.Model.Project
{
    public class SearchProjectsRequest
    {
        [Display("Account ID")]
        public string? AccountId { get; set; }

        [Display("Sort")]
        [StaticDataSource(typeof(SortByDataHandler))]
        public string? SortBy { get; set; }
    }
}
