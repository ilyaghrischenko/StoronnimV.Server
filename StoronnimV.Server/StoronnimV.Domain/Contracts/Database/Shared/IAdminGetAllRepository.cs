using StoronnimV.Domain.Projections.Shared;

namespace StoronnimV.Domain.Contracts.Database.Shared;

public interface IAdminGetAllRepository<TProjection> where TProjection : BaseProjection
{
    Task<IEnumerable<TProjection>?> GetAllForAdminAsync(CancellationToken ct); 
}