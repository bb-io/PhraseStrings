namespace Apps.PhraseStrings.Constants;

public static class RateLimits
{
    public const int RetryCount = 6;

    public const int QuotaBaseBackoffSeconds = 1;
    public const int QuotaMaxBackoffSeconds = 16;

    public const int ConcurrencyBaseBackoffSeconds = 2;
    public const int ConcurrencyMaxBackoffSeconds = 30;

    public const int MaxRetryDelaySeconds = 60;
    public const int MinJitterMilliseconds = 500;

    public const string ConcurrencyLimitMarker = "concurrency limit";

    public const string RetryAfterHeader = "Retry-After";
    public const string RateLimitRemainingHeader = "X-Rate-Limit-Remaining";
    public const string RateLimitResetHeader = "X-Rate-Limit-Reset";
    public const string TmsRateLimitRemainingHeader = "Ratelimit-Remaining";
    public const string TmsRateLimitResetHeader = "Ratelimit-Reset";
}
