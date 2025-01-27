using StoronnimV.Application.DTO.Responses.MusicPage;
using StoronnimV.Application.DTO.Responses.SchedulePage;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IMusicControllerService
{
    Task<MusicResponse> GetItemByIdAsync(long id, CancellationToken ct);
    Task<IEnumerable<MusicResponse>> GetAllAsync(CancellationToken ct);
}