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
            NewsResponse newsItem = await _newsControllerService.GetItemByIdAsync(id, ct);
            
            return Ok(newsItem);
        }
        
        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<PaginationResponse<NewsShortResponse>>> GetNewsForPage([FromRoute] int page, CancellationToken ct, [FromQuery] int pageSize = 9)
        {
            var newsPaginationResponse = await _newsControllerService.GetForPageAsync(page, pageSize, ct);
            
            return Ok(newsPaginationResponse);
        }
    }
}
