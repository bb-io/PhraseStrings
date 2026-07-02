using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Translation;

public class QualityScoreListResponse
{
    [JsonProperty("data")]
    public QualityScoreDataResponse? Data { get; set; }

    [JsonProperty("errors")]
    public List<QualityScoreErrorResponse>? Errors { get; set; }
}

public class QualityScoreDataResponse
{
    [JsonProperty("translations")]
    public List<QualityScoreResponse>? Translations { get; set; }
}

public class QualityScoreResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("engine")]
    public string? Engine { get; set; }

    [JsonProperty("score")]
    public double? Score { get; set; }
}

public class QualityScoreErrorResponse
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("code")]
    public string? Code { get; set; }
}
