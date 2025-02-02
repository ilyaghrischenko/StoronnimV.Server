using AutoMapper;
using Microsoft.Extensions.Logging;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.DTO.Responses.GroupPage.ShortGroupPage;
using StoronnimV.Application.DTO.Responses.GroupPage.ShortMember;
using StoronnimV.Application.Interfaces.Controllers;
using StoronnimV.Application.Interfaces.Entities;
using StoronnimV.Domain.Projections;

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
        _logger.LogInformation($"Service: GroupPageControllerService Method: GetGroupPageInfoAsync started at {DateTime.UtcNow}");
        
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
        
        _logger.LogInformation($"Service: GroupPageControllerService Method: GetGroupPageInfoAsync ended at {DateTime.UtcNow}");
        
        return groupPageFullInfoDto;
    }

    public async Task<MemberFullInfoResponse> GetMemberAsync(long memberId, CancellationToken ct)
    {
        _logger.LogInformation($"Service: GroupPageControllerService Method: GetMemberInfoAsync with memberId: {memberId} started at {DateTime.UtcNow}");
        
        var memberTask = _memberService.GetItemByIdAsync(memberId, ct);
        var socialsTask = _socialService.GetAllForMemberAsync(memberId, ct);
        
        await Task.WhenAll(memberTask, socialsTask);
        
        object member = await memberTask;
        var socials = await socialsTask;
        
        var memberDto = _mapper.Map<MemberResponse>(member);
        var socialsDto = _mapper.Map<IEnumerable<SocialResponse>>(socials);
        
        MemberFullInfoResponse memberFullInfoDto = new()
        {
            Member = memberDto,
            Socials = socialsDto
        };
        
        _logger.LogInformation($"Service: GroupPageControllerService Method: GetMemberInfoAsync with memberId: {memberId} ended at {DateTime.UtcNow}");
        
        return memberFullInfoDto;
    }
}