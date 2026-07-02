using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.PhraseStrings.Model.InteroperableXliff;

public class DownloadKeysResponse
{
    [Display("Content")]
    public FileReference Content { get; set; } = new();

    [Display("Total keys downloaded")]
    public int TotalKeysDownloaded { get; set; }

    [Display("Keys without target translations")]
    public int TotalKeysWithoutTargetTranslations { get; set; }

    [Display("Keys requiring review")]
    public int TotalKeysWithUnverifiedTargetTranslations { get; set; }

    [Display("Verified target keys")]
    public int TotalKeysWithVerifiedTargetTranslations { get; set; }

    [Display("Reviewed target keys")]
    public int TotalKeysWithReviewedTargetTranslations { get; set; }

    [Display("Source locale ID")]
    public string SourceLocaleId { get; set; } = string.Empty;

    [Display("Target locale ID")]
    public string TargetLocaleId { get; set; } = string.Empty;
}

public class UploadKeysResponse
{
    [Display("Keys uploaded")]
    public int KeysUploaded { get; set; }

    [Display("Project ID")]
    public string ProjectId { get; set; } = string.Empty;

    [Display("Source locale ID")]
    public string SourceLocaleId { get; set; } = string.Empty;

    [Display("Target locale ID")]
    public string TargetLocaleId { get; set; } = string.Empty;

    [Display("Job ID")]
    public string? JobId { get; set; }
}
