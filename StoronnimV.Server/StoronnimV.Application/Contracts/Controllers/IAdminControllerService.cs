using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Contracts.Controllers;

public interface IAdminControllerService
{
    Task DeleteNewsItemAsync(long id, CancellationToken ct);
}