using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Instruction.Query.CheckInstructionAcces;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.CheckRecipeIngredientAcces;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Mapper.Instruction;
using MyRecipes.Web.API.Mapper.RecipeIngredient;
using MyRecipes.Web.API.Models.Class.Instruction;

namespace MyRecipes.Web.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Constant.ROLE.ADMINANDUSER)]
    [Route(Constant.CONTROLLER_ROUTE.RECIPE_INSTRUCTION)]
    public class RecipeInstructionController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<RecipeInstructionController> _logger;
        public RecipeInstructionController(ISender mediator, ILogger<RecipeInstructionController> logger)
        {
            _sender = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllInstructionList()
        {
            var result = await _sender.Send(new GetAllInstructionQuery());
            _logger.LogInformation("GetAllInstructionList : finish without error");
            return Ok(result.ToInstructionResponse());
        }

        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetInstructionById(string Id)
        {

            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(GetInstructionById), Path.GetFileName("RecipeInstructionController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "GetInstructionById : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var result = await _sender.Send(guid.ToInstructionByIdQuery());
                _logger.LogInformation("GetInstructionById : finish without error");
                return Ok(result.ToInstructionResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateInstruction(CreateInstructionModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException(nameof(CreateInstruction), Path.GetFileName("RecipeInstructionController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "CreateInstruction : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.MODEL);
                }
                await _sender.Send(model.ToCommand());
                _logger.LogInformation("CreateInstruction : finish without error");
                return Created();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionAlreadyExisteException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionAlreadyExisteException(ex.Error, ex.Message);
            }

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateInstructionList(List<CreateInstructionModel> model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException(
                        nameof(CreateInstructionList),
                        Path.GetFileName("RecipeInstructionController"),
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        "CreateInstructionList : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.MODEL);
                }
                if (model.IsNullOrEmpty())
                {
                    throw new WrongParameterException(
                        nameof(CreateInstructionList),
                        Path.GetFileName("RecipeInstructionController"),
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        "CreateInstructionList : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.MODEL);
                }
                await _sender.Send(model.ToCommand());
                _logger.LogInformation("CreateInstructionList : finish without error");
                return Created();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionAlreadyExisteException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionAlreadyExisteException(ex.Error, ex.Message);
            }
        }

        [HttpPut]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> UpdateInstruction(UpdateInstructionModel model, string Id)
        {

            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(UpdateInstruction),
                        Path.GetFileName("RecipeInstructionController"),
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        "UpdateInstruction : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException(
                        nameof(UpdateInstruction),
                        Path.GetFileName("RecipeInstructionController"),
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        "UpdateInstruction : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.MODEL);
                }
                if (!this.CheckIsAdmin())
                {
                    if (!await _sender.Send(new CheckInstructionAccesQuery(guid, this.GetUserGuid())))
                        throw new ForbiddenAccessException(
                            nameof(UpdateInstruction),
                            Path.GetFileName("RecipeInstructionController"), 
                            Constant.EXCEPTION.TITLE.FORBIDDEN,
                            "UpdateInstruction : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.FORBIDDEN);
                }
                await _sender.Send(model.ToCommand(guid));
                _logger.LogInformation("UpdateInstruction : finish without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
            catch (InstructionAlreadyExisteException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionAlreadyExisteException(ex.Error, ex.Message);
            }
            catch (ForbiddenAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ForbiddenAccessException(ex.Error, ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> DeleteInstructionById(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(
                        nameof(DeleteInstructionById),
                        Path.GetFileName("RecipeInstructionController"),
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        "DeleteInstructionById : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                if (!this.CheckIsAdmin())
                {
                    if (!await _sender.Send(new CheckInstructionAccesQuery(guid, this.GetUserGuid())))
                        throw new ForbiddenAccessException(
                        nameof(DeleteInstructionById),
                        Path.GetFileName("RecipeInstructionController"),
                        Constant.EXCEPTION.TITLE.FORBIDDEN,
                        "DeleteInstructionById : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.FORBIDDEN);
                }
                await _sender.Send(guid.ToDeleteInstructionCommand());
                _logger.LogInformation("DeleteInstructionById : finish without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (InstructionNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
            catch (ForbiddenAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ForbiddenAccessException(ex.Error, ex.Message);
            }
        }
    }
}
