using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.Contracts.Controllers;
using StoronnimV.Application.DTO.Responses.GroupPage;

namespace StoronnimV.Api.Controllers
{
    /// <summary>
    /// Контроллер для страницы 'Группа', он позволяет доставать нужные данные для отображения
    /// </summary>
    /// <param name="groupPageControllerService"></param>
    [Route("api/group")]
    [ApiController]
    public class GroupPageController(IGroupPageControllerService groupPageControllerService,
        ILogger<GroupPageController> logger) : ControllerBase
    {
        private readonly IGroupPageControllerService _groupPageControllerService = groupPageControllerService;
        private readonly ILogger<GroupPageController> _logger = logger;
        
        [HttpGet]
        public async Task<ActionResult<GroupPageFullInfoResponse>> GetGroupPageInfo(CancellationToken ct)
        {
            GroupPageFullInfoResponse groupPage = await _groupPageControllerService.GetGroupPageInfoAsync(ct);
            
            return Ok(groupPage);
            
        }
        
        [HttpGet("member/{memberId:long}")]
        public async Task<ActionResult<MemberFullInfoResponse>> GetMember([FromRoute] long memberId, CancellationToken ct)
        {
            MemberFullInfoResponse member = await _groupPageControllerService.GetMemberAsync(memberId, ct);
            
            return Ok(member);
        }
    }
}
