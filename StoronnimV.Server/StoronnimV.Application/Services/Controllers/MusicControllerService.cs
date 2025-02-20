using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.MusicPage;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Domain.Projections;

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
        MusicPlatformProjection musicPlatformItem = await _musicPlatformService.GetItemByIdAsync(id, ct);

        var musicPlatformDto = _mapper.Map<MusicResponse>(musicPlatformItem);
        
        return musicPlatformDto;
    }

    public async Task<IEnumerable<MusicResponse>> GetAllAsync(CancellationToken ct)
    {
        var musicPlatforms = await _musicPlatformService.GetAllAsync(ct);

        var musicPlatformsDto = _mapper.Map<IEnumerable<MusicResponse>>(musicPlatforms);
        
        return musicPlatformsDto;
    }
}