using Apps.PhraseStrings.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Project;

public class ProjectRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; } = string.Empty;
}
