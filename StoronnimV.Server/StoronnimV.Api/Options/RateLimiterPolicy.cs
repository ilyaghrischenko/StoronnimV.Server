namespace StoronnimV.Api.Options;

public class RateLimiterPolicy
{
    public required string PolicyName { get; init; }
    public required int Limit { get; init; }
    public required TimeSpan Expiration { get; init; }
}