using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Controllers.Shared;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Application.Models;

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
        
        PaginationResult paginationResult = await _newsService.GetForAdminPageAsync(page, pageSize, ct);

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

        PaginationResult paginationResult = await _videoService.GetForAdminPageAsync(page, pageSize, ct);

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
}