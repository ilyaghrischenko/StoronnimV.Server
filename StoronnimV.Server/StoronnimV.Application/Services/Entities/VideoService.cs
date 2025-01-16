using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Application.Services.Entities;

public class VideoService(
    IVideoRepository videoRepository,
    ILogger<VideoService> logger)
    : IVideoService
{
    private readonly IVideoRepository _videoRepository = videoRepository;
    private readonly ILogger<VideoService> _logger = logger;
    
    public async Task<object> GetItemByIdAsync(long id)
    {
        _logger.LogInformation($"Service: VideoService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        var video = await _videoRepository.GetByIdAsNoTrackingAsync(id)
            ?? throw new EntityNotFoundException($"Video with id: {id} was not found");
        
        _logger.LogInformation($"Service: VideoService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return video;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        _logger.LogInformation($"Service: VideoService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var videos = await _videoRepository.GetAllAsync();
        
        _logger.LogInformation($"Service: VideoService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return videos ?? new List<object>();
        
    }
}