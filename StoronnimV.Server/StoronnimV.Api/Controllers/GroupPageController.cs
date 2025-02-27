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
    public class GroupPageController(IGroupPageControllerService groupPageControllerService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GroupPageFullInfoResponse>> GetGroupPageInfo(CancellationToken ct)
        {
            GroupPageFullInfoResponse groupPage = await groupPageControllerService.GetGroupPageInfoAsync(ct);
            
            return Ok(groupPage);
            
        }
        
        [HttpGet("member/{memberId:long}")]
        public async Task<ActionResult<MemberFullInfoResponse>> GetMember([FromRoute] long memberId, CancellationToken ct)
        {
            MemberFullInfoResponse member = await groupPageControllerService.GetMemberAsync(memberId, ct);
            
            return Ok(member);
        }
    }
}
