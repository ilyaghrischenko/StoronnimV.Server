using StoronnimV.Application.Contracts.Entities.Shared;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Domain.Projections;

namespace StoronnimV.Application.Contracts.Entities;

public interface IMusicPlatformService
    : IGetByIdService<MusicPlatformProjection>, IGetAllService<MusicPlatformProjection>
{
    Task AddMusicPlatformAsync(MusicPlatformAdditionRequest request, CancellationToken ct);
    Task DeleteMusicPlatformAsync(long id, CancellationToken ct);

}