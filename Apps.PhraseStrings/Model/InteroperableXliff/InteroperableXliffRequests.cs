using Apps.PhraseStrings.DataHandlers;
using Apps.PhraseStrings.DataHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.PhraseStrings.Model.InteroperableXliff;

public class DownloadKeysRequest
{
    [Display("Target locale ISO code or ID")]
    public string TargetLocale { get; set; } = string.Empty;

    [Display("Source locale ISO code or ID")]
    public string? SourceLocale { get; set; }

    [Display("Job ID")]
    [DataSource(typeof(JobDataHandler))]
    public string? JobId { get; set; }

    [Display("Key IDs")]
    public IEnumerable<string>? KeyIds { get; set; }

    [Display("Key names")]
    public IEnumerable<string>? KeyNames { get; set; }

    [Display("Branch")]
    [DataSource(typeof(BranchDataHandler))]
    public string? Branch { get; set; }
}

public class UploadKeysRequest
{
    public FileReference Content { get; set; } = new();

    [Display("XLIFF states to apply to target translations")]
    [StaticDataSource(typeof(XliffStateDataHandler))]
    public IEnumerable<string>? StatesToApplyToTargetTranslations { get; set; }

    [Display("Add tags to uploaded keys")]
    public string? Tags { get; set; }

    [Display("Branch")]
    public string? Branch { get; set; }

    [Display("Target locale ID")]
    public string? TargetLocaleId { get; set; }

    [Display("Project ID")]
    public string? ProjectId { get; set; }
}
