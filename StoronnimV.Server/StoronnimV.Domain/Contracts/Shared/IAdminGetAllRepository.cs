using StoronnimV.Domain.Projections.Member;
using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Contracts.Shared;

public interface IAdminGetAllRepository<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>?> GetAllForAdminAsync(CancellationToken ct); 
}