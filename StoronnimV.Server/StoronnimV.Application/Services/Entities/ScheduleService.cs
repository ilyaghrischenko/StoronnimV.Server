using System.Globalization;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.Exceptions;
using StoronnimV.Application.Models;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections.Schedule;

namespace StoronnimV.Application.Services.Entities;

/// <summary>
/// Сервис для проверки полученных данных, полученых с репозитория
/// </summary>
/// <param name="scheduleRepository"></param>
public class ScheduleService(
    IScheduleRepository scheduleRepository,
    IBlobRepository blobRepository) : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    private readonly IBlobRepository _blobRepository = blobRepository;
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

        if (allSchedules == null || !allSchedules.Any())
        {
            return;
        }
        
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

    public async Task<PaginationResult<ScheduleShortProjection>> GetForPageAsync(int page, int pageSize, CancellationToken ct, params object[] args)
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
    
    /// <summary>
    /// Schedule addition to database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    public async Task AddScheduleAsync(ScheduleAdditionRequest request, CancellationToken ct)
    {
        Schedule schedule = new()
        {
            Title = request.Title,
            PerformanceDateTime = DateTime.ParseExact(request.PerformanceDateTime, "dd.MM.yyyy HH.mm", CultureInfo.InvariantCulture),
            Description = request.Description,
            Location = request.Location,
            Photo = null,
            Status = Enum.Parse<ScheduleStatus>(request.Status)
        };
        
        await _scheduleRepository.AddAsync(schedule, ct);
        
        if (request.Photo != null)
        {
            string photoUrl = await _blobRepository.AddFileAndGetUrlAsync("storonnimv-photo", $"schedule-{schedule.Id}", request.Photo.OpenReadStream(), ct);
            await _scheduleRepository.UpdateAsync(schedule, () => schedule.Photo = photoUrl, ct);
        }
    }
    
    /// <summary>
    /// Schedule deletion from database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public async Task DeleteScheduleAsync(long id, CancellationToken ct)
    {
        Schedule? schedule = await _scheduleRepository.GetByIdAsync(id, ct);

        if (schedule is null)
        {
            throw new EntityNotFoundException($"Schedule with {nameof(id)}: {id} was not found");
        }

        await _scheduleRepository.DeleteAsync(schedule, ct);

        if (schedule.Photo != null)
        {
            await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"schedule-{id}", ct);
        }
    }
    
    //todo: update schedule
    // public async Task UpdateScheduleAsync(ScheduleUpdateRequest request, CancellationToken ct)
}