using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Responses.Admin;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Contracts.Controllers;

public interface IAdminControllerService
{
    Task<IEnumerable<BasicAdminResponse>> GetAllBasicAdminsAsync(CancellationToken ct);
    
    Task DeleteBasicAdminAsync(long id, CancellationToken ct);
    Task AddBasicAdminAsync(CreateBasicAdminRequest request, CancellationToken ct);
    
    Task DeleteNewsItemAsync(long id, CancellationToken ct);
    Task DeleteScheduleAsync(long id, CancellationToken ct);
    Task DeleteVideoAsync(long id, CancellationToken ct);
    Task DeleteGroupPageAsync(long id, CancellationToken ct);
    Task DeleteMemberAsync(long id, CancellationToken ct);
    Task DeleteMusicPlatformAsync(long id, CancellationToken ct);
    Task DeleteSocialAsync(long id, CancellationToken ct);
}