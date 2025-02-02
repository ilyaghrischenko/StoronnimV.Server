using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.Interfaces.Controllers.Shared;

namespace StoronnimV.Application.Interfaces.Controllers;

public interface ISchedulesControllerService
    : IGetByIdControllerService<ScheduleResponse>, IGetAllControllerService<ScheduleShortResponse>
{
    
}