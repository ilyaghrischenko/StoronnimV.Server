using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="scheduleRepository"></param>
public class ScheduleService(IScheduleRepository scheduleRepository) : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    
    public async Task<ScheduleFullProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        ScheduleFullProjection schedule = await _scheduleRepository.GetByIdAsNoTrackingAsync(id, ct)
                                          ?? throw new EntityNotFoundException($"Schedule with {nameof(id)}: {id} was not found");
        
        return schedule;
    }

    public async Task UpdateStatusesAsync(CancellationToken ct)
    {
        var allSchedules = await _scheduleRepository
            .GetAllSchedulesAsync(ct);
        
        DateTime today = DateTime.UtcNow.Date;
        
        var schedulesToChange = allSchedules
            .Where(schedule =>schedule.Status == ScheduleStatus.Active
            && schedule.PerformanceDateTime < today)
            .ToList();
        
        var updateTasks = schedulesToChange.Select(schedule =>
            _scheduleRepository.UpdateAsync(schedule, () =>
            {
                schedule.Status = ScheduleStatus.Passed;
            }, ct)
        );
        
        await Task.WhenAll(updateTasks);
    }

    public async Task<PaginationResult<ScheduleShortProjection>>GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
    {
        if (page <= 0)
        {
            throw new PaginationException("Invalid page number");
        }

        int totalCount = await _scheduleRepository.GetTotalCountAsync(ct);

        try
        {
            if (totalCount == 0)
            {
                throw new PaginationException(string.Empty);
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await _scheduleRepository.GetForPageAsync(page, ct, pageSize);
            
            if (items is null || !items.Any())
            {
                throw new PaginationException(string.Empty);
            }

            PaginationResult<ScheduleShortProjection> respone = new()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalCount,
                Items = items.ToList()
            };

            return respone;
        }
        catch (PaginationException)
        {
            return new PaginationResult<ScheduleShortProjection>
            {
                CurrentPage = page,
                TotalPages = 0,
                TotalItems = 0,
                Items = []
            };
        }
    }
}