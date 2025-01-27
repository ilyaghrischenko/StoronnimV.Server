using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.NewsPage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;

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
    
    public async Task<PaginationResponse<NewsResponse>> GetForPageAsync(int page, int pageSize, params object[] args)
    {
        _logger.LogInformation($"Service: AdminControllerService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] started at {DateTime.UtcNow}");
        
        var paginationResult = await _newsService.GetForAdminPageAsync(page, pageSize);

        var newsDto = _mapper.Map<IEnumerable<NewsResponse>>(paginationResult.Items);

        var response = new PaginationResponse<NewsResponse>(
            currentPage: paginationResult.CurrentPage,
            totalPages: paginationResult.TotalPages,
            totalItems: paginationResult.TotalItems,
            items: newsDto
        );
        
        _logger.LogInformation($"Service: AdminControllerService Method: GetForPageAsync with [page: {page}, pageSize: {pageSize}] ended at {DateTime.UtcNow}");

        return response;
    }
}