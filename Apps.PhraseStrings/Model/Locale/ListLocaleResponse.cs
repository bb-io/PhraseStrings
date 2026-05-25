using Blackbird.Applications.Sdk.Common;

namespace Apps.PhraseStrings.Model.Locale;

public class ListLocaleResponse
{
    public IEnumerable<LocaleResponse> Locales { get; set; } = [];

    [Display("Locale IDs")]
    public IEnumerable<string> LocaleIds => Locales.Select(locale => locale.Id);

    [Display("Locale codes")]
    public IEnumerable<string> LocaleCodes => Locales.Select(locale => locale.Code);
}
