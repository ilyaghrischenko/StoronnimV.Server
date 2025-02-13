using StoronnimV.Domain.Contracts.Database.Shared;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Domain.Contracts.Database;

public interface ISocialRepository
    : IRepository<Social>, IGetByIdRepository<SocialProjection>
{
    // public Task<IEnumerable<SocialProjection>?> GetAllForMemberAsync(long memberId, CancellationToken ct);
}