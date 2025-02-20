using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Services.Controllers;

/// <summary>
/// Сервис для маппинга данных с бд и возвращения контроллеру
/// </summary>
/// <param name="scheduleService"></param>
/// <param name="mapper"></param>
public class SchedulesControllerService(
    IScheduleService scheduleService,
    IMapper mapper,
    ILogger<SchedulesControllerService> logger) : ISchedulesControllerService
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<SchedulesControllerService> _logger = logger;
    
    public async Task<ScheduleResponse> GetItemByIdAsync(long id, CancellationToken ct)
    {
        ScheduleFullProjection schedule = await _scheduleService.GetItemByIdAsync(id, ct);
        
        var scheduleDto = _mapper.Map<ScheduleResponse>(schedule);
        
        return scheduleDto;
    }

    public async Task<IEnumerable<ScheduleShortResponse>> GetAllAsync(CancellationToken ct)
    {
        var schedules = await _scheduleService.GetAllAsync(ct);
        
        var schedulesDto = _mapper.Map<IEnumerable<ScheduleShortResponse>>(schedules);
        
        return schedulesDto;
    }
}