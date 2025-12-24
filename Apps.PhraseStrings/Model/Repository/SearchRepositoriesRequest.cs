using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Repository;

public class SearchRepositoriesRequest
{
    [Display("Ignore inactive repository syncs", 
        Description = "Do not include inactive repository syncs in the response. False by default")]
    public bool? IgnoreInactiveRepos { get; set; }
}
