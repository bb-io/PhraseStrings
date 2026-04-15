using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Screenshot;

public class ScreenshotResponse(ScreenshotDtoResponse dtoResponse)
{
    [Display("Screenshot ID")]
    public string Id { get; set; } = dtoResponse.Id;

    [Display("Screenshot name")]
    public string Name { get; set; } = dtoResponse.Name;

    [Display("Screenshot description")]
    public string Description { get; set; } = dtoResponse.Description;

    [Display("Screenshot URL")]
    public string? ScreenshotUrl { get; set; } = dtoResponse.ScreenshotUrl?.AbsoluteUri;

    [Display("Created at")]
    public DateTime CreatedAt { get; set; } = dtoResponse.CreatedAt;

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; } = dtoResponse.UpdatedAt;

    [Display("Markers count")]
    public int MarkersCount { get; set; } = dtoResponse.MarkersCount;
}
