using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.DTO.Responses.Video;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Projections.Video;

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
        VideoShortProjection video = await _videoService.GetItemByIdAsync(id, ct);
        
        var videoDto = _mapper.Map<VideoPageShortResponse>(video);
        
        return videoDto;
    }
    
    public async Task<PaginationResponse<VideoPageShortResponse>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        string type = (string)args[0];
        
        PaginationResult<VideoShortProjection> paginationResult = await _videoService.GetForPageAsync(page, pageSize, ct, type);
        
        var videosDto = _mapper.Map<IEnumerable<VideoPageShortResponse>>(paginationResult.Items);
        
        var paginationResponse = new PaginationResponse<VideoPageShortResponse>
        {
            CurrentPage = paginationResult.CurrentPage,
            TotalPages = paginationResult.TotalPages,
            TotalItems = paginationResult.TotalItems,
            Items = videosDto
        };
        
        return paginationResponse;
    }
}