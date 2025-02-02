using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Interfaces.Shared;
using StoronnimV.Domain.Projections.Social;

namespace StoronnimV.Domain.Interfaces;

public interface ISocialRepository
    : IRepository<Social>, IGetByIdRepository<SocialShortProjection>
{
    public Task<IEnumerable<SocialShortProjection>?> GetAllForMemberAsync(long memberId, CancellationToken ct);
}