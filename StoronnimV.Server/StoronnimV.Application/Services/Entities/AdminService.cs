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
    private readonly IPasswordHasher<Admin> _passwordHasher = passwordHasher;
    
    public async Task<AdminProjection> GetItemByIdAsync(long id, CancellationToken ct)
    {
        AdminProjection? admin = await _adminRepository.GetByIdAsNoTrackingAsync(id, ct);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Admin with id: {id} was not found");
        }
        
        return admin;
    }

    public async Task<IEnumerable<BasicAdminProjection>> GetAllBasicAdminsAsync(CancellationToken ct)
    {
        var basicAdmins = await _adminRepository.GetAllBasicAdminsAsync(ct);

        return basicAdmins ?? new List<BasicAdminProjection>();
    }

    public async Task DeleteBasicAdminAsync(long id, CancellationToken ct)
    {
        Admin? basicAdmin = await _adminRepository.GetByIdAsync(id, ct);

        if (basicAdmin is null)
        {
            throw new EntityNotFoundException($"Basic Admin with id: {id} was not found");
        }
        
        await _adminRepository.DeleteAsync(basicAdmin, ct);
    }

    public async Task AddBasicAdminAsync(string login, string unhashedPassword, CancellationToken ct)
    {
        string hashedPassword = _passwordHasher.HashPassword(null! ,unhashedPassword);

        Admin newBasicAdmin = new()
        {
            Login = login,
            Password = hashedPassword
        };
        
        await _adminRepository.AddAsync(newBasicAdmin, ct);
    }

    public async Task EditBasicAdminLoginAsync(long id, string newlogin, CancellationToken ct)
    {
        Admin? adminToChange = await _adminRepository.GetByIdAsync(id, ct);

        if (adminToChange is null)
        {
            throw new EntityNotFoundException($"Admin with id: {id} was not found");
        }

        await _adminRepository.UpdateAsync(adminToChange, () =>
        {
            adminToChange.Login = newlogin;
        }, ct);
    }

    public async Task EditBasicAdminPasswordAsync(long id, string oldPassword, string newUnhashedPassword, CancellationToken ct)
    {
        Admin? adminToChange = await _adminRepository.GetByIdAsync(id, ct);
        
        if (adminToChange is null)
        {
            throw new EntityNotFoundException($"Admin with id: {id} was not found");
        }
        
        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(adminToChange, adminToChange.Password, oldPassword);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("passwords do not match");
        }
        
        string newHashedPassword = _passwordHasher.HashPassword(null!, newUnhashedPassword);

        await _adminRepository.UpdateAsync(adminToChange, () =>
        {
            adminToChange.Password = newHashedPassword;
        }, ct);
    }

    public async Task DeleteNewsItemAsync(long id, CancellationToken ct)
    {
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
    }

    public async Task DeleteScheduleAsync(long id, CancellationToken ct)
    {
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
    }

    public async Task DeleteVideoAsync(long id, CancellationToken ct)
    {
        Video? video = await _videoRepository.GetByIdAsync(id, ct);

        if (video is null)
        {
            throw new EntityNotFoundException($"Video with id: {id} was not found");
        }
        
        await _videoRepository.DeleteAsync(video, ct);

        await _blobRepository.DeleteFileAsync("storonnimv-video", $"video-{id}", ct);
    }

    public async Task DeleteGroupPageAsync(long id, CancellationToken ct)
    {
        GroupPage? groupPage = await _groupPageRepository.GetByIdAsync(id, ct);

        if (groupPage is null)
        {
            throw new EntityNotFoundException($"Group page with id: {id} was not found");
        }
        
        await _groupPageRepository.DeleteAsync(groupPage, ct);
        
        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"group-page-{id}", ct);
    }

    public async Task DeleteMemberAsync(long id, CancellationToken ct)
    {
        Member? member = await _memberRepository.GetByIdAsync(id, ct);

        if (member is null)
        {
            throw new EntityNotFoundException($"Member with id: {id} was not found");
        }
        
        await _memberRepository.DeleteAsync(member, ct);
        
        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"member-{id}", ct);
    }

    public async Task DeleteMusicPlatformAsync(long id, CancellationToken ct)
    {
        MusicPlatform? musicPlatform = await _musicPlatformRepository.GetByIdAsync(id, ct);

        if (musicPlatform is null)
        {
            throw new EntityNotFoundException($"Music platform with id: {id} was not found");
        }
        
        await _musicPlatformRepository.DeleteAsync(musicPlatform, ct);

        await _blobRepository.DeleteAllFilesByNameAsync("storonnimv-photo", $"music-platform-{id}", ct);
    }

    public async Task DeleteSocialAsync(long id, CancellationToken ct)
    {
        Social? social = await _socialRepository.GetByIdAsync(id, ct);

        if (social is null)
        {
            throw new EntityNotFoundException($"Social with id: {id} was not found");
        }

        await _socialRepository.DeleteAsync(social, ct);
    }
}