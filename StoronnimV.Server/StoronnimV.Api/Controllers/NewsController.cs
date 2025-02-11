using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для страницы 'Новости', он позволяет доставать нужные данные для отображения
    /// </summary>
    /// <param name="newsControllerService"></param>
    [Route("api/news")]
    [ApiController]
    public class NewsController(INewsControllerService newsControllerService,
        ILogger<NewsController> logger) : ControllerBase
    {
        private readonly INewsControllerService _newsControllerService = newsControllerService;
        private readonly ILogger<NewsController> _logger = logger;

        [HttpGet("{id:long}")]
        public async Task<ActionResult<NewsResponse>> GetNewsItem([FromRoute] long id, CancellationToken ct)
        {
            _logger.LogInformation($"Controller: NewsController Method: GetNewsItem with id: {id} started at {DateTime.UtcNow}");
            
            NewsResponse newsItem = await _newsControllerService.GetItemByIdAsync(id, ct);
            
            _logger.LogInformation($"Controller: NewsController Method: GetNewsItem with id: {id} ended at {DateTime.UtcNow}");
            
            return Ok(newsItem);
        }
        
        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<PaginationResponse<NewsShortResponse>>> GetNewsForPage([FromRoute] int page, CancellationToken ct, [FromQuery] int pageSize = 9)
        {
            _logger.LogInformation($"Controller: NewsController Method: GetNewsForPage with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
            
            var newsPaginationResponse = await _newsControllerService.GetForPageAsync(page, pageSize, ct);
            
            _logger.LogInformation($"Controller: NewsController Method: GetNewsForPage with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");
            
            return Ok(newsPaginationResponse);
        }
    }
}
