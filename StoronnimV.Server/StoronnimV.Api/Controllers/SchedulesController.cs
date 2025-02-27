using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Domain.Entities;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для страницы 'Афиша', он позволяет доставать нужные данные для отображения
    /// </summary>
    /// <param name="schedulesControllerService"></param>
    [Route("api/schedules")]
    [ApiController]
    public class SchedulesController(ISchedulesControllerService schedulesControllerService) : ControllerBase
    {
        private readonly ISchedulesControllerService _schedulesControllerService = schedulesControllerService;

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ScheduleResponse>> GetSchedule([FromRoute] long id, CancellationToken ct)
        {
            ScheduleResponse schedule = await _schedulesControllerService.GetItemByIdAsync(id, ct);
            
            return Ok(schedule);
        }

        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<PaginationResponse<ScheduleShortResponse>>> GetSchedulesForPage(
            [FromRoute] int page, CancellationToken ct, [FromQuery] int pageSize = 5)
        {
            var schedulesPaginationResponse = await _schedulesControllerService.GetForPageAsync(page, pageSize, ct);

            return Ok(schedulesPaginationResponse);
        }
    }
}