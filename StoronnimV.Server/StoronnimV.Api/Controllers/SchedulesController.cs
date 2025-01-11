using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.Interfaces.Controllers;
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
        public async Task<ActionResult<ScheduleResponse>> GetSchedule([FromRoute] long id)
        {
            _logger.LogInformation($"Controller: SchedulesController Method: GetSchedule with id: {id} started at {DateTime.UtcNow}");
            
            var schedule = await _schedulesControllerService.GetItemByIdAsync(id);
            
            _logger.LogInformation($"Controller: SchedulesController Method: GetSchedule with id: {id} ended at {DateTime.UtcNow}");
            
            return Ok(schedule);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleShortResponse>>> GetSchedules()
        {
            _logger.LogInformation($"Controller: SchedulesController Method: GetSchedules started at {DateTime.UtcNow}");
            
            var schedules = await _schedulesControllerService.GetAllAsync();
            
            _logger.LogInformation($"Controller: SchedulesController Method: GetSchedules ended at {DateTime.UtcNow}");
            
            return Ok(schedules);
        }
    }
}