using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections.Social;

namespace StoronnimV.Domain.Contracts;

public interface ISocialRepository
    : IRepository<Social>, IGetByIdRepository<SocialShortProjection>
{
    public Task<IEnumerable<SocialShortProjection>?> GetAllForMemberAsync(long memberId, CancellationToken ct);
}