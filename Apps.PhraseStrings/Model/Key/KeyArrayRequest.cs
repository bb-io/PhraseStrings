using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Key
{
    public class KeyArrayRequest
    {
        [Display("Key IDs")]
        [DataSource(typeof(KeyDataHandler))]
        public IEnumerable<string> KeyIds { get; set; } = [];
    }
}
