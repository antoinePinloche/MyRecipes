using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Authentification.Application.User.Command.CreateUser;
using MyRecipes.Authentification.Application.User.Command.DeleteUser;
using MyRecipes.Authentification.Application.User.Command.UpdatePassword;
using MyRecipes.Authentification.Application.User.Query.GetAllUsers;
using MyRecipes.web.Models.Class;

namespace MyRecipes.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthentificationController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> ActualUserInformation()
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Register(CreateUserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _sender.Send(new CreateUserCommand(user.UserName, user.Email, user.Password));
            return Created();
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> ModifyUser(CreateUserModel user)
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
            Guid guidSend;
            if (!Guid.TryParse(guid, out guidSend))
            {
                return BadRequest("DeleteUser : BadParameter" + guid);
            }
            await _sender.Send(new DeleteUserCommand(guidSend));
            return Ok();
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> UpdatePassword(string password)
        {
            await _sender.Send(new UpdatePasswordCommand(password, Guid.NewGuid()));
            return Ok();
        }
    }
}
