using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Authentification.Application.User.Command.DeleteUser;
using MyRecipes.Authentification.Application.User.Command.UpdateUserRole;
using MyRecipes.Authentification.Application.User.Query.GetAllUsers;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;

namespace MyRecipes.web.Controllers
{

    [ApiController]
    [Authorize(Roles = Constant.ROLE.ADMIN)]
    [Route(Constant.CONTROLLER_ROUTE.ADMIN_USER)]
    public class AdminUserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<AdminUserController> _logger;

        public AdminUserController(ISender mediator, ILogger<AdminUserController> logger)
        {
            _sender = mediator;
            _logger = logger;
        }

        [HttpPut]
        [Route("User/{Id}/Role/{NewRole}")]
        public async Task<IActionResult> ModifyUserRole(string Id, string NewRole, bool ToAdd = true)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    _logger.LogError("ModifyUserRole : wrong Id parameter");
                    throw new WrongParameterException(nameof(ModifyUserRole), Path.GetFileName("AdminUserController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "ModifyUserRole : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                await _sender.Send(new UpdateUserRoleCommand(guid, NewRole, ToAdd));
                _logger.LogInformation("ModifyUserRole : Finish without error");
                return Ok();
            }
            catch(WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new UserNotFoundException(ex.Error, ex.Message);
            }
            catch (UserRoleAlreadyExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new UserNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetAllUsers()
        {
            var tmp = await _sender.Send(new GetAllUsersQuery());
            _logger.LogInformation("GetAllUsers : return All users");
            return Ok(tmp);
        }

        [HttpDelete]
        [Route("User/{guid}")]
        public async Task<IActionResult> DeleteUser(string guid)
        {
            try
            {
                if (!Guid.TryParse(guid, out Guid guidSend))
                {
                    _logger.LogError("DeleteUser : wrong Id parameter");
                    throw new WrongParameterException(nameof(DeleteUser), Path.GetFileName("AdminUserController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "DeleteUser : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }

                await _sender.Send(new DeleteUserCommand(guidSend));
                _logger.LogInformation("DeleteUser : Complete without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new UserNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }

        }
    }
}
