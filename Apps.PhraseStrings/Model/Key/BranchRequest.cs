using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Key;
public class BranchRequest
{
    [Display("Branch")]
    [DataSource(typeof(BranchDataHandler))]
    public string? Branch { get; set; }
}
