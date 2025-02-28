namespace StoronnimV.Api.Options;

public class RateLimiterOptions
{
    public required int StatusCode { get; init; }
    public required List<RateLimiterPolicy> Policies { get; init; }
}