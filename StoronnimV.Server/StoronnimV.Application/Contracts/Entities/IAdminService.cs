using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Admin;

namespace StoronnimV.Application.Contracts.Entities;

public interface IAdminService : IGetByIdService<AdminProjection>
{
    Task<IEnumerable<BasicAdminProjection>> GetAllBasicAdminsAsync(CancellationToken ct);

    Task DeleteBasicAdminAsync(long id, CancellationToken ct);
    Task AddBasicAdminAsync(string login, string unhashedPassword, CancellationToken ct);
    Task EditBasicAdminLoginAsync(long id, string newlogin, CancellationToken ct);
    Task EditBasicAdminPasswordAsync(long id, string oldPassword, string newUnhashedPassword, CancellationToken ct);
    
    Task DeleteNewsItemAsync(long id, CancellationToken ct);
    Task DeleteScheduleAsync(long id, CancellationToken ct);
    Task DeleteVideoAsync(long id, CancellationToken ct);
    Task DeleteGroupPageAsync(long id, CancellationToken ct);
    Task DeleteMemberAsync(long id, CancellationToken ct);
    Task DeleteMusicPlatformAsync(long id, CancellationToken ct);
    Task DeleteSocialAsync(long id, CancellationToken ct);
}