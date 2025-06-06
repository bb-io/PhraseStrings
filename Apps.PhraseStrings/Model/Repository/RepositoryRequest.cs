using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Repository
{
    public class RepositoryRequest
    {
        [Display("Repository ID")]
        [DataSource(typeof(RepositoryDataHandler))]
        public string RepositoryId { get; set; }
    }
}
