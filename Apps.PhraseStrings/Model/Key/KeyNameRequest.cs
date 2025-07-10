using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Key;

public class KeyNameRequest
{
    [Display("Key name")]
    public string KeyName { get; set; } = string.Empty;
}
