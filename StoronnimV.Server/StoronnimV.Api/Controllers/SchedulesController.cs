using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для страницы 'Афиша', он позволяет доставать нужные данные для отображения
    /// </summary>
    /// <param name="schedulesControllerService"></param>
    [Route("api/schedules")]
    [ApiController]
    public class SchedulesController(ISchedulesControllerService schedulesControllerService,
        ILogger<SchedulesController> logger) : ControllerBase
    {
        private readonly ISchedulesControllerService _schedulesControllerService = schedulesControllerService;
        private readonly ILogger<SchedulesController> _logger = logger;

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ScheduleResponse>> GetSchedule([FromRoute] long id, CancellationToken ct)
        {
            ScheduleResponse schedule = await _schedulesControllerService.GetItemByIdAsync(id, ct);
            
            return Ok(schedule);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleShortResponse>>> GetSchedules(CancellationToken ct)
        {
            var schedules = await _schedulesControllerService.GetAllAsync(ct);
            
            return Ok(schedules);
        }
    }
}