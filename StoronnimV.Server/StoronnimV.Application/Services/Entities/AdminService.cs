using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Requests.Account;
using StoronnimV.Application.Exceptions;
using StoronnimV.Domain.Contracts;
using StoronnimV.Domain.Contracts.AzureBlobStorage;
using StoronnimV.Domain.Contracts.Database;
using StoronnimV.Domain.Entities;
using StoronnimV.Domain.Projections;

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
        _logger.LogInformation($"Service: AdminService Method: GetItemByIdAsync with id: {id} started at {DateTime.UtcNow}");

        AdminProjection? admin = await _adminRepository.GetByIdAsNoTrackingAsync(id, ct);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Admin with id: {id} was not found");
        }
        
        _logger.LogInformation($"Service: AdminService Method: GetItemByIdAsync with id: {id} ended at {DateTime.UtcNow}");

        return admin;
    }

    public async Task<IEnumerable<AdminProjection>> GetAllAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: GetAllAsync started at {DateTime.UtcNow}");
        
        var admins = await _adminRepository.GetAllAsNoTrackingAsync(ct);

        if (admins is null || !admins.Any())
        {
            return new List<AdminProjection>();
        }
        
        _logger.LogInformation($"Service: AdminService Method: GetAllAsync ended at {DateTime.UtcNow}");

        return admins;
    }

    public async Task<Admin> LogInAsync(LogInRequest request, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: LogInAsync started at {DateTime.UtcNow}");
        
        Admin? admin = await _adminRepository.GetByLoginAsync(request.Login, ct);

        if (admin is null)
        {
            throw new LogInException($"Admin with login: {request.Login} was not found");
        }
        
        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(admin, admin.Password, request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new LogInException("Wrong password");
        }
        
        _logger.LogInformation($"Service: AdminService Method: LogInAsync started at {DateTime.UtcNow}");

        return admin;
    }

    public async Task DeleteNewsItemAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteNewsItemAsync started at {DateTime.UtcNow}");

        News? newsItem = await _newsRepository.GetByIdAsync(id, ct);

        if (newsItem is null)
        {
            throw new EntityNotFoundException($"NewsItem with id: {id} was not found");
        }
        
        await _newsRepository.DeleteAsync(newsItem, ct);

        if (newsItem.Photo != null)
        {
            await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"news-{id}", ct);
        }

        //TODO!! удалять ли видео с блоба для каждой новости? или просто в методе удаления самого видео удалять с блоба
        if (newsItem.Video != null)
        {
            await _blobRepository.DeleteFileAsync("storonnimv-video", $"video-{newsItem.Video.Id}", ct);
        }

        _logger.LogInformation($"Service: AdminService Method: DeleteNewsItemAsync ended at {DateTime.UtcNow}");
    }

    public async Task DeleteScheduleAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteScheduleAsync started at {DateTime.UtcNow}");
        
        Schedule? schedule = await _scheduleRepository.GetByIdAsync(id, ct);

        if (schedule is null)
        {
            throw new EntityNotFoundException($"Schedule with id: {id} was not found");
        }
        
        await _scheduleRepository.DeleteAsync(schedule, ct);

        if (schedule.Photo != null)
        {
            await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"schedule-{id}", ct);
        }
        
        _logger.LogInformation($"Service: AdminService Method: DeleteScheduleAsync ended at {DateTime.UtcNow}");
    }

    public async Task DeleteVideoAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteVideoAsync started at {DateTime.UtcNow}");
        
        Video? video = await _videoRepository.GetByIdAsync(id, ct);

        if (video is null)
        {
            throw new EntityNotFoundException($"Video with id: {id} was not found");
        }
        
        await _videoRepository.DeleteAsync(video, ct);

        await _blobRepository.DeleteFileAsync("storonnimv-video", $"video-{id}", ct);
        
        _logger.LogInformation($"Service: AdminService Method: DeleteVideoAsync ended at {DateTime.UtcNow}");
    }

    public async Task DeleteGroupPageAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteGroupPageAsync started at {DateTime.UtcNow}");

        GroupPage? groupPage = await _groupPageRepository.GetByIdAsync(id, ct);

        if (groupPage is null)
        {
            throw new EntityNotFoundException($"Group page with id: {id} was not found");
        }
        
        await _groupPageRepository.DeleteAsync(groupPage, ct);
        
        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"group-page-{id}", ct);
        
        _logger.LogInformation($"Service: AdminService Method: DeleteGroupPageAsync ended at {DateTime.UtcNow}");
    }

    public async Task DeleteMemberAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteMemberAsync started at {DateTime.UtcNow}");
        
        Member? member = await _memberRepository.GetByIdAsync(id, ct);

        if (member is null)
        {
            throw new EntityNotFoundException($"Member with id: {id} was not found");
        }
        
        await _memberRepository.DeleteAsync(member, ct);
        
        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"member-{id}", ct);
        
        _logger.LogInformation($"Service: AdminService Method: DeleteMemberAsync ended at {DateTime.UtcNow}");
    }

    public async Task DeleteMusicPlatformAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteMusicPlatformAsync started at {DateTime.UtcNow}");

        MusicPlatform? musicPlatform = await _musicPlatformRepository.GetByIdAsync(id, ct);

        if (musicPlatform is null)
        {
            throw new EntityNotFoundException($"Music platform with id: {id} was not found");
        }
        
        await _musicPlatformRepository.DeleteAsync(musicPlatform, ct);

        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"music-platform-{id}", ct);
        
        _logger.LogInformation($"Service: AdminService Method: DeleteMusicPlatformAsync ended at {DateTime.UtcNow}");
    }

    public async Task DeleteSocialAsync(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Service: AdminService Method: DeleteSocialAsync started at {DateTime.UtcNow}");
        
        Social? social = await _socialRepository.GetByIdAsync(id, ct);

        if (social is null)
        {
            throw new EntityNotFoundException($"Social with id: {id} was not found");
        }

        await _socialRepository.DeleteAsync(social, ct);
        
        _logger.LogInformation($"Service: AdminService Method: DeleteSocialAsync ended at {DateTime.UtcNow}");
    }
}