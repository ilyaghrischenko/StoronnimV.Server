using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Projections;

public class AdminProjection : BaseProjection
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}