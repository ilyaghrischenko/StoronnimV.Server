using StoronnimV.Application.Interfaces.Entities.Shared;
using StoronnimV.Domain.Projections.Social;

namespace StoronnimV.Application.Interfaces.Entities;

public interface ISocialService : IGetByIdService<SocialShortProjection>
{
    public Task<IEnumerable<object>> GetAllForMemberAsync(long memberId, CancellationToken ct);
}