using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface ISocialService : IGetByIdService<SocialProjection>
{
    // public Task<IEnumerable<SocialProjection>> GetAllForMemberAsync(long memberId, CancellationToken ct);
}