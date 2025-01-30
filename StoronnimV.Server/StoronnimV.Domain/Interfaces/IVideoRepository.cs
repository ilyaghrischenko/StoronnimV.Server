using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Interfaces.Shared;

namespace StoronnimV.Domain.Interfaces;

public interface IVideoRepository 
    : IRepository<Video>, IReceivableRepository, IPaginationRepository, IAdminPaginationRepository
{
    public Task<object?> GetPromotionVideoForHomePageAsync(CancellationToken ct);
    public Task<int> GetTotalCountForAdminPageAsync(CancellationToken ct);
}