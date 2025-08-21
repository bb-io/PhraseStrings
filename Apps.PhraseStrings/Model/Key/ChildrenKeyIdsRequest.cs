using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Key
{
    public class ChildrenKeyIdsRequest
    {
        [Display("Key IDs to link")]
        [DataSource(typeof(KeyDataHandler))]
        public IEnumerable<string> KeyIds { get; set; } = [];
    }
}
