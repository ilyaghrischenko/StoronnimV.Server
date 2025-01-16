using StoronnimV.Application.DTO.Responses.Video;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IVideoControllerService
{
    Task<VideoPageResponse> GetItemByIdAsync(long id);
    Task<IEnumerable<VideoPageResponse>> GetAllAsync();
}