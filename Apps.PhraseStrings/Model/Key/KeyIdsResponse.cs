using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Key;

public class KeyIdsResponse
{
    [Display("Key IDs")]
    public IEnumerable<string> KeyIds { get; set; } = [];
}
