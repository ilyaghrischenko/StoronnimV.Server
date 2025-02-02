using StoronnimV.Application.DTO.Responses.MusicPage;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface IMusicControllerService
    : IGetByIdControllerService<MusicResponse>, IGetAllControllerService<MusicResponse>
{
    
}