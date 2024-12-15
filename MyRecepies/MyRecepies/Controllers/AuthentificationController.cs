using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyRecepies.Authentification.Application.User.Command.CreateUser;
using MyRecepies.Authentification.Application.User.Query.GetAllUsers;
using MyRecepies.web.Models.Class;

namespace MyRecepies.web.Controllers
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
        public async Task<IActionResult> CreateUser(CreateUserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _sender.Send(new CreateUserCommand(user.UserName, user.Email, user.Password, user.FirstName, user.LastName));
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
    }
}
