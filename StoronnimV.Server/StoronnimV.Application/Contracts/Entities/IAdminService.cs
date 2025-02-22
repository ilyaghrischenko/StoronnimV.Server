using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Admin;

namespace StoronnimV.Application.Contracts.Entities;

public interface IAdminService : IGetByIdService<AdminProjection>
{
    Task DeleteNewsItemAsync(long id, CancellationToken ct);
    Task DeleteScheduleAsync(long id, CancellationToken ct);
    Task DeleteVideoAsync(long id, CancellationToken ct);
    Task DeleteGroupPageAsync(long id, CancellationToken ct);
    Task DeleteMemberAsync(long id, CancellationToken ct);
    Task DeleteMusicPlatformAsync(long id, CancellationToken ct);
    Task DeleteSocialAsync(long id, CancellationToken ct);
}