using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Project;
public class GetProjectLocaleFromCodeRequest
{
    [Display("Locale ISO code (eg, 'en-GB')")]
    public string LocaleCode { get; set; } = string.Empty;
}
