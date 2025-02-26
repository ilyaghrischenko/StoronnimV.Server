using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.DTO.Requests.Entities.Pages.Addition;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Enums;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Admin;

namespace StoronnimV.Application.Services.Entities;

public class AdminService(
    IAdminRepository adminRepository,
    INewsRepository newsRepository,
    IScheduleRepository scheduleRepository,
    IVideoRepository videoRepository,
    IGroupPageRepository groupPageRepository,
    IMemberRepository memberRepository,
    IMusicPlatformRepository musicPlatformRepository,
    ISocialRepository socialRepository,
    IBlobRepository blobRepository,
    ILogger<AdminService> logger,
    IPasswordHasher<Admin> passwordHasher) : IAdminService
{
    private readonly IAdminRepository _adminRepository = adminRepository;
    private readonly INewsRepository _newsRepository = newsRepository;
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
    private readonly IVideoRepository _videoRepository = videoRepository;
    private readonly IGroupPageRepository _groupPageRepository = groupPageRepository;
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly IMusicPlatformRepository _musicPlatformRepository = musicPlatformRepository;
    private readonly ISocialRepository _socialRepository = socialRepository;
    private readonly IBlobRepository _blobRepository = blobRepository;
    private readonly ILogger<AdminService> _logger = logger;
    private readonly IPasswordHasher<Admin> _passwordHasher = passwordHasher;

    public async Task<AdminProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        AdminProjection? admin = await _adminRepository.GetByIdAsNoTrackingAsync(id, ct);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Admin with {nameof(id)}: {id} was not found");
        }

        return admin;
    }
}