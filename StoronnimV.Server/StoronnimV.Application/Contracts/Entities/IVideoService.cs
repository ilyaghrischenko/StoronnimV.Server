using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Contracts.Entities;

public interface IVideoService 
    : IGetByIdService<VideoShortProjection>, IPaginationService<VideoShortProjection>
{
    Task AddVideoAsync(VideoAdditionRequest request, CancellationToken ct);
    Task DeleteVideoAsync(long id, CancellationToken ct);

}