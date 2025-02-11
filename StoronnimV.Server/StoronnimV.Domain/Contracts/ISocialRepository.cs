using StoronnimV.Domain.Contracts.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Contracts;

public interface ISocialRepository
    : IRepository<Social>, IGetByIdRepository<SocialProjection>
{
    public Task<IEnumerable<SocialProjection>?> GetAllForMemberAsync(long memberId, CancellationToken ct);
}