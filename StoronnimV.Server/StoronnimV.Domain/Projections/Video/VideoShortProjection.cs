using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Projections.Video;

public class VideoShortProjection : BaseProjection
{
    public required string Title { get; init; }
    public required string Url { get; init; }
}