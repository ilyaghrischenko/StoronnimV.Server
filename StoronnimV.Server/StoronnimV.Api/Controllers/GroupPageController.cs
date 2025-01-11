using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoronnimV.Application.DTO.Responses.GroupPage;
using StoronnimV.Application.Interfaces.Controllers;

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
        public async Task<ActionResult<GroupPageFullInfoResponse>> GetGroupPageInfoAsync()
        {
            _logger.LogInformation($"Controller: GroupPageController Method: GetGroupPageInfoAsync started at {DateTime.UtcNow}");
            
            var groupPage = await _groupPageControllerService.GetGroupPageInfoAsync();
            
            _logger.LogInformation($"Controller: GroupPageController Method: GetGroupPageInfoAsync ended at {DateTime.UtcNow}");
            
            return Ok(groupPage);
            
        }
        
        [HttpGet("member/{memberId:long}")]
        public async Task<ActionResult<MemberFullInfoResponse>> GetMemberInfoAsync([FromRoute] long memberId)
        {
            _logger.LogInformation($"Controller: GroupPageController Method: GetMemberInfoAsync started at {DateTime.UtcNow}");
            
            var member = await _groupPageControllerService.GetMemberInfoAsync(memberId);
            
            _logger.LogInformation($"Controller: GroupPageController Method: GetMemberInfoAsync ended at {DateTime.UtcNow}");
            
            return Ok(member);
        }
    }
}
