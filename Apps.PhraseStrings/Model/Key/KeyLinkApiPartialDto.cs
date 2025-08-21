using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Key;

// Partial DTO for the key link response (we only need child ids)
public class KeyLinkApiPartialDto
{
    [JsonProperty("children")]
    public List<KeyLinkChild>? Children { get; set; }
}

public class KeyLinkChild
{
    [JsonProperty("id")]
    public string KeyId { get; set; } = string.Empty;
}