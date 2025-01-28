using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.Extensions;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Models;

namespace StoronnimV.Application.Services.Controllers;

/// <summary>
/// Сервис для маппинга данных с бд и возвращения контроллеру
/// </summary>
/// <param name="newsService"></param>
/// <param name="mapper"></param>
public class NewsControllerService(
    INewsService newsService,
    IMapper mapper,
    ILogger<NewsControllerService> logger) : INewsControllerService
{
    private readonly INewsService _newsService = newsService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<NewsControllerService> _logger = logger;

    public async Task<NewsResponse> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: NewsControllerService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        object newsItem = await _newsService.GetItemByIdAsync(id, ct);

        var newsItemDto = _mapper.Map<NewsResponse>(newsItem);
        
        _logger.LogInformation($"Service: NewsControllerService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");
        
        return newsItemDto;
    }

    public async Task<IEnumerable<NewsResponse>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: NewsControllerService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var sortedNews = await _newsService.GetAllAsync(ct);

        var newsDto = _mapper.Map<IEnumerable<NewsResponse>>(sortedNews);
        
        _logger.LogInformation($"Service: NewsControllerService Method: GetAllAsync ended at {DateTime.UtcNow}");
        
        return newsDto;
    }

    public async Task<PaginationResponse<NewsShortResponse>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Service: NewsControllerService Method: GetForPageAsync with page: {page} started at {DateTime.UtcNow}");
        
        PaginationResult paginationResult = await _newsService.GetForPageAsync(page, pageSize, ct);
        
        var newsDto = _mapper.Map<IEnumerable<NewsShortResponse>>(paginationResult.Items);
        
        var response = new PaginationResponse<NewsShortResponse>(
            currentPage: paginationResult.CurrentPage,
            totalPages: paginationResult.TotalPages,
            totalItems: paginationResult.TotalItems,
            items: newsDto
        );
        
        _logger.LogInformation($"Service: NewsControllerService Method: GetForPageAsync with page: {page} ended at {DateTime.UtcNow}");

        return response;
    }
}