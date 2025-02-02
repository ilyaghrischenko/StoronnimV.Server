using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.MusicPage;
using StoronnimV.Application.DTO.Responses.SchedulePage;

namespace StoronnimV.Application.Services.Controllers;

public class MusicControllerService(
    IMusicPlatformService musicPlatformService,
    IMapper mapper,
    ILogger<MusicControllerService> logger) : IMusicControllerService
{
    private readonly IMusicPlatformService _musicPlatformService = musicPlatformService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<MusicControllerService> _logger = logger;
    
    public async Task<MusicResponse> GetItemByIdAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: MusicControllerService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        object musicPlatformItem = await _musicPlatformService.GetItemByIdAsync(id, ct);

        var musicPlatformDto = _mapper.Map<MusicResponse>(musicPlatformItem);
        
        _logger.LogInformation($"Service: MusicControllerService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        return musicPlatformDto;
    }

    public async Task<IEnumerable<MusicResponse>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: MusicControllerService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var musicPlatforms = await _musicPlatformService.GetAllAsync(ct);

        var musicPlatformsDto = _mapper.Map<IEnumerable<MusicResponse>>(musicPlatforms);
        
        _logger.LogInformation($"Service: MusicControllerService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return musicPlatformsDto;
    }
}