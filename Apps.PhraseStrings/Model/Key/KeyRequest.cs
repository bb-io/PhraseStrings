using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Key
{
    public class KeyRequest
    {
        [Display("Key ID")]
        [DataSource(typeof(KeyDataHandler))]
        public string KeyId { get; set; } = string.Empty;
    }
}
