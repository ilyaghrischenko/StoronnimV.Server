using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IAdminService : IGetByIdService<AdminProjection>, IGetAllService<AdminProjection>
{
    public Task<Admin> LogInAsync(LogInRequest request, CancellationToken ct);
    
    public Task DeleteNewsItemAsync(long id, CancellationToken ct);
    public Task DeleteScheduleAsync(long id, CancellationToken ct);
    public Task DeleteVideoAsync(long id, CancellationToken ct);
    public Task DeleteGroupPageAsync(long id, CancellationToken ct);
    public Task DeleteMemberAsync(long id, CancellationToken ct);
    public Task DeleteMusicPlatformAsync(long id, CancellationToken ct);
    public Task DeleteSocialAsync(long id, CancellationToken ct);
}