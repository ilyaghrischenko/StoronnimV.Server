using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Projections.News;
using StoronnimV.Domain.Projections.Video;

namespace StoronnimV.Application.Services.Controllers;

public class AdminControllerService(
    INewsService newsService,
    IVideoService videoService,
    IMapper mapper,
    ILogger<AdminControllerService> logger) : IAdminControllerService
{
    private readonly INewsService _newsService = newsService;
    private readonly IVideoService _videoService = videoService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AdminControllerService> _logger = logger;
    
    public async Task<PaginationResponse<NewsResponse>> GetNewsForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: GetNewsForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        PaginationResult<NewsFullProjection> paginationResult = await _newsService.GetForAdminPageAsync(page, pageSize, ct);

        var newsDto = _mapper.Map<IEnumerable<NewsResponse>>(paginationResult.Items);

        var response = new PaginationResponse<NewsResponse>
        {
            CurrentPage = paginationResult.CurrentPage,
            TotalPages = paginationResult.TotalPages,
            TotalItems = paginationResult.TotalItems,
            Items = newsDto
        };
        
        _logger.LogInformation($"Service: AdminControllerService Method: GetNewsForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return response;
    }

    public async Task<PaginationResponse<VideoPageResponse>> GetVideosForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: GetVideosForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        PaginationResult<VideoFullProjection> paginationResult = await _videoService.GetForAdminPageAsync(page, pageSize, ct);

        var videosDto = _mapper.Map<IEnumerable<VideoPageResponse>>(paginationResult.Items);

        var response = new PaginationResponse<VideoPageResponse>
        {
            CurrentPage = paginationResult.CurrentPage,
            TotalPages = paginationResult.TotalPages,
            TotalItems = paginationResult.TotalItems,
            Items = videosDto
        };
        
        _logger.LogInformation($"Service: AdminControllerService Method: GetVideosForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");

        return response;
    }

    public async Task<IEnumerable<NewsResponse>> GetNewsItemsByTitleAsync(string title, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: GetNewsItemByTitleAsync with title: {title} started at {DateTime.UtcNow}");

        var newsProjections = await _newsService.GetItemsByTitleAsync(title, ct);

        var newsDto = _mapper.Map<IEnumerable<NewsResponse>>(newsProjections);
        
        _logger.LogInformation($"Service: AdminControllerService Method: GetNewsItemByTitleAsync with title: {title} ended at {DateTime.UtcNow}");
        
        return newsDto;
    }

    public async Task<IEnumerable<VideoPageResponse>> GetVideosByTitleAsync(string title, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: GetVideosByTitleAsync with title: {title} started at {DateTime.UtcNow}");

        var videosProjections = await _videoService.GetItemsByTitleAsync(title, ct);

        var videosDto = _mapper.Map<IEnumerable<VideoPageResponse>>(videosProjections);
        
        _logger.LogInformation($"Service: AdminControllerService Method: GetVideosByTitleAsync with title: {title} ended at {DateTime.UtcNow}");

        return videosDto;
    }
}