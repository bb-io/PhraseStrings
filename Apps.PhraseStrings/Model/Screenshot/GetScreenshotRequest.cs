using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Screenshot;

public class GetScreenshotRequest
{
    [Display("Screenshot ID")]
    public string? ScreenshotID { get; set; }

    [Display("Screenshot name")]
    public string? ScreenshotName { get; set; }
}
