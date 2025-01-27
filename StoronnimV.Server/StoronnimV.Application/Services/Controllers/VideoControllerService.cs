using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;

namespace StoronnimV.Application.Services.Controllers;

public class VideoControllerService(
    IVideoService videoService,
    ILogger<VideoControllerService> logger,
    IMapper mapper)
    : IVideoControllerService
{
    private readonly IVideoService _videoService = videoService;
    private readonly ILogger<VideoControllerService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    
    public async Task<VideoPageShortResponse> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: VideoControllerService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");

        var video = await _videoService.GetItemByIdAsync(id, ct);
        
        var videoDto = _mapper.Map<VideoPageShortResponse>(video);
        
        _logger.LogInformation($"Service: VideoControllerService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return videoDto;
    }

    public async Task<IEnumerable<VideoPageShortResponse>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: VideoControllerService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var videos = await _videoService.GetAllAsync(ct);
        
        var videosDto = _mapper.Map<IEnumerable<VideoPageShortResponse>>(videos);
        
        _logger.LogInformation($"Service: VideoControllerService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return videosDto;
    }

    public async Task<PaginationResponse<VideoPageShortResponse>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        _logger.LogInformation($"Service: VideoControllerService Method: GetForPageAsync with page: {page} started at {DateTime.UtcNow}");
        
        var type = (string)args[0];
        
        var paginationResult = await _videoService.GetForPageAsync(page, pageSize, ct, type);
        
        var newsDto = _mapper.Map<IEnumerable<VideoPageShortResponse>>(paginationResult.Items);
        
        var paginationResponse = new PaginationResponse<VideoPageShortResponse>(
            currentPage: paginationResult.CurrentPage,
            totalPages: paginationResult.TotalPages,
            totalItems: paginationResult.TotalItems,
            items: newsDto
        );
        
        _logger.LogInformation($"Service: VideoControllerService Method: GetForPageAsync with page: {page} ended at {DateTime.UtcNow}");

        return paginationResponse;
    }
}