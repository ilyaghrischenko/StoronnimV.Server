using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;

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
        _logger.LogInformation($"Service: SchedulesControllerService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");
        
        object schedule = await _scheduleService.GetItemByIdAsync(id, ct);
        
        var scheduleDto = _mapper.Map<ScheduleResponse>(schedule);
        
        _logger.LogInformation($"Service: SchedulesControllerService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");
        
        return scheduleDto;
    }

    public async Task<IEnumerable<ScheduleShortResponse>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: SchedulesControllerService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var schedules = await _scheduleService.GetAllAsync(ct);
        
        var schedulesDto = _mapper.Map<IEnumerable<ScheduleShortResponse>>(schedules);
        
        _logger.LogInformation($"Service: SchedulesControllerService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return schedulesDto;
    }
}