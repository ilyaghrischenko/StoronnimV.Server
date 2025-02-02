using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections.Social;

namespace StoronnimV.Application.Contracts.Entities;

public interface ISocialService : IGetByIdService<SocialShortProjection>
{
    public Task<IEnumerable<SocialShortProjection>> GetAllForMemberAsync(long memberId, CancellationToken ct);
}