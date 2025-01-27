using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IVideoControllerService :  IPaginationControllerService<PaginationResponse<VideoPageShortResponse>>
{
    Task<VideoPageShortResponse> GetItemByIdAsync(long id);
    Task<IEnumerable<VideoPageShortResponse>> GetAllAsync();
}