using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.SchedulePage;
using StoronnimV.Application.DTO.Responses.Shared;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Services.Controllers;

/// <summary>
/// Сервис для маппинга данных с бд и возвращения контроллеру
/// </summary>
/// <param name="scheduleService"></param>
/// <param name="mapper"></param>
public class SchedulesControllerService(
    IScheduleService scheduleService,
    IMapper mapper) : ISchedulesControllerService
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ScheduleResponse> GetItemByIdAsync(long id, CancellationToken ct)
    {
        ScheduleFullProjection schedule = await _scheduleService.GetItemByIdAsync(id, ct);
        
        var scheduleDto = _mapper.Map<ScheduleResponse>(schedule);
        
        return scheduleDto;
    }

    public async Task<PaginationResponse<ScheduleShortResponse>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        PaginationResult<ScheduleShortProjection> paginationResult = await _scheduleService.GetForPageAsync(page, pageSize, ct);

        var schedulesDto = _mapper.Map<IEnumerable<ScheduleShortResponse>>(paginationResult.Items);

        var response = new PaginationResponse<ScheduleShortResponse>
        {
            CurrentPage = paginationResult.CurrentPage,
            TotalPages = paginationResult.TotalPages,
            TotalItems = paginationResult.TotalItems,
            Items = schedulesDto
        };
        
        return response;
    }
}