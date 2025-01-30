using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IVideoControllerService :  IPaginationControllerService<VideoPageShortResponse>
{
    Task<VideoPageShortResponse> GetItemByIdAsync(long id, CancellationToken ct);
    Task<IEnumerable<VideoPageShortResponse>> GetAllAsync(CancellationToken ct);
}