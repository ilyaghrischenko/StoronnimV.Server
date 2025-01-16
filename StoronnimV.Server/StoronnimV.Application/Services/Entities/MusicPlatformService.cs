using Microsoft.Extensions.Logging;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Interfaces;

namespace StoronnimV.Application.Services.Entities;

public class MusicPlatformService(IMusicPlatformRepository musicPlatformRepository,
    ILogger<MusicPlatformService> logger) : IMusicPlatformService
{
    private readonly IMusicPlatformRepository _musicPlatformRepository = musicPlatformRepository;
    private readonly ILogger<MusicPlatformService> _logger = logger;

    public async Task<object> GetItemByIdAsync(long id)
    {
        _logger.LogInformation($"Service: MusicPlatformService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        var musicPlatform = await _musicPlatformRepository.GetByIdAsNoTrackingAsync(id)
            ?? throw new EntityNotFoundException($"Music Platform with id: {id} was not found");
        
        _logger.LogInformation($"Service: MusicPlatformService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");
        
        return musicPlatform;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        _logger.LogInformation($"Service: MusicPlatformService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var allMusicPlatforms = await _musicPlatformRepository.GetAllAsync();
        if (allMusicPlatforms is null || !allMusicPlatforms.Any())
        {
            return new List<object>();
        }
        
        _logger.LogInformation($"Service: MusicPlatformService Method: GetAllAsync ended at {DateTime.UtcNow}");
        
        return allMusicPlatforms
            .ToList();
    }
}