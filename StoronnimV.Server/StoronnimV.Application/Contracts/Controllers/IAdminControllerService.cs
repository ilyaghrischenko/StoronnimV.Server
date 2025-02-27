using StoronnimV.Application.DTO.Requests.Entities.Admin;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.DTO.Responses.Admin;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Contracts.Controllers;

public interface IAdminControllerService
{
    Task DeleteNewsItemAsync(long id, CancellationToken ct);
    Task DeleteScheduleAsync(long id, CancellationToken ct);
    Task DeleteVideoAsync(long id, CancellationToken ct);
    Task DeleteGroupPageAsync(long id, CancellationToken ct);
    Task DeleteMemberAsync(long id, CancellationToken ct);
    Task DeleteMusicPlatformAsync(long id, CancellationToken ct);
    Task DeleteSocialAsync(long id, CancellationToken ct);
    
    Task AddNewsItemAsync(NewsItemAdditionRequest request, CancellationToken ct);
    Task AddScheduleAsync(ScheduleAdditionRequest request, CancellationToken ct);
    Task AddVideoAsync(VideoAdditionRequest request, CancellationToken ct);
    Task AddGroupPageAsync(GroupPageAdditionRequest request, CancellationToken ct);
    Task AddMemberAsync(MemberAdditionRequest request, CancellationToken ct);
    Task AddMusicPlatformAsync(MusicPlatformAdditionRequest request, CancellationToken ct);
    Task AddSocialAsync(SocialAdditionRequest request, CancellationToken ct);
}