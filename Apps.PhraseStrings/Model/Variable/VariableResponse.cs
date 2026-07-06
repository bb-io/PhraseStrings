using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Variable;

public class VariableResponse
{
    [JsonProperty("name")]
    [Display("Variable name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("value")]
    [Display("Value")]
    public string? Value { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    [Display("Updated at")]
    public DateTime? UpdatedAt { get; set; }
}
