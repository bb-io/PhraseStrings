using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Project;
public class GetProjectLocalesRequest
{
    [Display("Return target locales only")]
    public bool ReturnTargetLocalesOnly { get; set; }
}
