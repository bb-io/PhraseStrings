using Apps.PhraseStrings.DataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Model.Variable;

public class VariableRequest
{
    [Display("Variable name")]
    [DataSource(typeof(VariableDataHandler))]
    public string Name { get; set; } = string.Empty;
}

public class VariableValueRequest
{
    [Display("Value")]
    public string Value { get; set; } = string.Empty;
}
