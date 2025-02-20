using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.Contracts.Entities;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;
using StoronnimV.Application.DTO.Responses.GroupPage.ShortMember;
using StoronnimV.Domain.Projections;
using StoronnimV.Domain.Projections.Member;

namespace StoronnimV.Application.Services.Controllers;

/// <summary>
/// Сервис для маппинга данных с бд и возвращения контроллеру
/// </summary>
/// <param name="groupPageService"></param>
/// <param name="memberService"></param>
/// <param name="socialService"></param>
/// <param name="mapper"></param>
public class GroupPageControllerService(
    IGroupPageService groupPageService,
    IMemberService memberService,
    ISocialService socialService,
    IMapper mapper,
    ILogger<GroupPageControllerService> logger) : IGroupPageControllerService
{
    private readonly IGroupPageService _groupPageService = groupPageService;
    private readonly IMemberService _memberService = memberService;
    private readonly ISocialService _socialService = socialService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GroupPageControllerService> _logger = logger;
    
    public async Task<GroupPageFullInfoResponse> GetGroupPageInfoAsync(CancellationToken ct)
    {
        var groupPageTask = _groupPageService.GetFirstGroupPageAsync(ct);
        var membersTask = _memberService.GetAllAsync(ct);
        
        await Task.WhenAll(groupPageTask, membersTask);
        
        GroupPageProjection groupPage = await groupPageTask;
        var members = await membersTask;
        
        var groupPageDto = _mapper.Map<GroupPageResponse>(groupPage);
        var membersShort = _mapper.Map<IEnumerable<MemberShortResponse>>(members);

        GroupPageFullInfoResponse groupPageFullInfoDto = new()
        {
            GroupPage = groupPageDto,
            Members = membersShort
        };
        
        return groupPageFullInfoDto;
    }

    public async Task<MemberFullInfoResponse> GetMemberAsync(long memberId, CancellationToken ct)
    {
        var member = await _memberService.GetItemByIdAsync(memberId, ct);
        
        var memberDto = _mapper.Map<MemberResponse>(member);
        var socialsDto = _mapper.Map<IEnumerable<SocialResponse>>(member.Socials);
        
        MemberFullInfoResponse memberFullInfoDto = new()
        {
            Id = memberDto.Id,
            PhotoUrl = memberDto.PhotoUrl,
            FullName = memberDto.FullName,
            Description = memberDto.Description,
            Role = memberDto.Role,
            Socials = socialsDto
        };
        
        return memberFullInfoDto;
    }
}