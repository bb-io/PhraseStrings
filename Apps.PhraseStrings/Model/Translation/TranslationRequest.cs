using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Translation
{
    public class TranslationRequest
    {
        [Display("Translation ID")]
        [DataSource(typeof(TranslationDataHandler))]
        public string? TranslationId { get; set; }
    }
}
