using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Locale
{
    public class LocaleRequest
    {
        [Display("Locale ID")]
        [DataSource(typeof(LocaleDataHandler))]
        public string LocaleId { get; set; }
    }
}
