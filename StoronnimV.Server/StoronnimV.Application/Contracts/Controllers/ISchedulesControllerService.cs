using StoronnimV.Application.Contracts.Controllers.Shared;
using StoronnimV.Application.DTO.Responses.SchedulePage;

namespace StoronnimV.Application.Contracts.Controllers;

public interface ISchedulesControllerService
    : IGetByIdControllerService<ScheduleResponse>, IGetAllControllerService<ScheduleShortResponse>
{
    
}