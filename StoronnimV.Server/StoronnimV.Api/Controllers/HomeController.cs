using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.HomePage;
using StoronnimV.Application.Interfaces.Controllers;

namespace StoronnimV.Api.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController(
        IHomeControllerService homeControllerService,
        ILogger<HomeController> logger) : ControllerBase
    {
        private readonly IHomeControllerService _homeControllerService = homeControllerService;
        private readonly ILogger<HomeController> _logger = logger;

        [HttpGet("news/{count:int}")]
        public async Task<ActionResult<IEnumerable<NewsHomeResponse>>> GetNews([FromRoute] int count)
        {
            _logger.LogInformation($"Controller: HomeController Method: GetNews with count: {count} started at {DateTime.UtcNow}");
            
            var newsDto = await _homeControllerService.GetNewsAsync(count);
            
            _logger.LogInformation($"Controller: HomeController Method: GetNews with count: {count} ended at {DateTime.UtcNow}");

            return Ok(newsDto);
        }

        [HttpGet("schedule")]
        public async Task<ActionResult<ScheduleHomeResponse>> GetSchedule()
        {
            _logger.LogInformation($"Controller: HomeController Method: GetSchedule started at {DateTime.UtcNow}");
            
            var scheduleDto = await _homeControllerService.GetScheduleAsync();
            
            _logger.LogInformation($"Controller: HomeController Method: GetSchedule ended at {DateTime.UtcNow}");

            return Ok(scheduleDto);
        }
    }
}
