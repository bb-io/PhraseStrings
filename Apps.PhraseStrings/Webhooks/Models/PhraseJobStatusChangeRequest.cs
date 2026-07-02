using Apps.PhraseStrings.DataHandlers.Static;
using Apps.PhraseStrings.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.PhraseStrings.Webhooks.Models;

public class PhraseJobStatusChangeRequest
{
    [Display("Events to react to")]
    [StaticDataSource(typeof(JobStatusChangeEventDataHandler))]
    public IEnumerable<string>? EventsToReactTo { get; set; }

    [Display("Project IDs")]
    [DataSource(typeof(ProjectDataHandler))]
    public IEnumerable<string>? ProjectIds { get; set; }

    [Display("Job IDs")]
    public IEnumerable<string>? JobIds { get; set; }

    [Display("Job name contains")]
    public string? JobNameContains { get; set; }

    [Display("Job name doesn't contain")]
    public string? JobNameDoesntContain { get; set; }
}
