using Apps.PhraseStrings.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Repository;

public class FindRepositoryRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string? ProjectId { get; set; }

    [Display("Repository name")]
    public string? RepositoryName { get; set; }
}
