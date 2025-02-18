using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Authentification.Application.User.Command.DeleteUser;
using MyRecipes.Authentification.Application.User.Query.GetAllUsers;
using MyRecipes.Transverse.Constant;

namespace MyRecipes.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Constant.ROLE.ADMIN)]
    public class AdminUserController : ControllerBase
    {
        private readonly ISender _sender;

        public AdminUserController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpPut]
        [Route("api/[controller]/[action]/User/{Guid}/Role/{NewRole}")]
        public async Task<IActionResult> ModifyUserRole(string Guid, string NewRole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return BadRequest();
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            var tmp = await _sender.Send(new GetAllUsersQueryRequest());
            return Ok(tmp);
        }

        [HttpDelete]
        [Route("api/[controller]/[action]/{guid}")]
        public async Task<IActionResult> DeleteUser(string guid)
        {
            if (!Guid.TryParse(guid, out Guid guidSend))
            {
                return BadRequest("DeleteUser : BadParameter" + guid);
            }
            await _sender.Send(new DeleteUserCommand(guidSend));
            return Ok();
        }
    }
}
