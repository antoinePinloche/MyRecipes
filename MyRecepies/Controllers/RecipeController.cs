using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Application.Recipe.Query.CheckRecipeAcces;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.Recipe.Query.GetMyRecipe;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Mapper.Instruction;
using MyRecipes.Web.API.Mapper.Recipe;
using MyRecipes.Web.API.Mapper.RecipeIngredient;
using MyRecipes.Web.API.Models.Class.Recipe;


namespace MyRecipes.Web.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Constant.ROLE.ADMINANDUSER)]
    [Route(Constant.CONTROLLER_ROUTE.RECIPE)]
    public class RecipeController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<RecipeController> _logger;
        public RecipeController(ISender mediator, ILogger<RecipeController> logger)
        {
            _sender = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRecipe()
        {
            try
            {
                var result = await _sender.Send(new GetAllRecipeQuery());
                _logger.LogInformation("GetAllRecipe : finish without error");
                return Ok(result.ToRecipeResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMyRecipe()
        {
            try
            {
                var result = await _sender.Send(new GetMyRecipeQuery(this.GetUserGuid()));
                _logger.LogInformation("GetAllRecipe : finish without error");
                return Ok(result.ToRecipeResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception();
            }
        }


        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetRecipeById(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(GetRecipeById), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "GetRecipeById : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var result = await _sender.Send(guid.ToRecipeByIdQuery());
                _logger.LogInformation("GetRecipeById : finish without error");
                return Ok(result.ToRecipeResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception();
            }
        }

        [HttpGet("{Id}/RecipeIngredient")]
        public async Task<IActionResult> GetRecipeIngredientByRecipeId(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(GetRecipeIngredientByRecipeId), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "GetRecipeIngredientByRecipeId : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                List<GetRecipeIngredientByRecipeIdQueryResult> result = await _sender.Send(guid.ToRecipeIngredientByRecipeIdQuery());
                _logger.LogInformation("GetRecipeIngredientByRecipeId : finish without error");
                return Ok(result.ToRecipeIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception();
            }
        }

        [HttpGet("{Id}/Instructions")]
        public async Task<IActionResult> GetInstructionByRecipeId(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(GetInstructionByRecipeId), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "GetInstructionByRecipeId : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                List<GetAllInstructionByRecipeIdQueryResult> result = await _sender.Send(guid.ToAllInstructionByRecipeIdQuery());
                _logger.LogInformation("GetInstructionByRecipeId : finish without error");
                return Ok(result.ToInstructionResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (InstructionNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpGet("[action]/{Name}")]
        public async Task<IActionResult> GetRecipeByName(string Name)
        {
            try
            {
                if (Name.IsNullOrEmpty() && Name.Count() < 3)
                    throw new WrongParameterException(nameof(GetRecipeByName), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "GetRecipeByName : parameter Name is too short or missing");
                var result = await _sender.Send(Name.ToRecipeByNameQuery());
                _logger.LogInformation("GetRecipeByName : finish without error");
                return Ok(result.ToRecipeResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRecipe(CreateRecipeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException(nameof(CreateRecipe), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "CreateRecipe : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.MODEL);
                }
                await _sender.Send(model.ToCommand(this.GetUserGuid()));
                _logger.LogInformation("CreateRecipe : finish without error");
                return Created();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new NotImplementedException();
            }
        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateRecipe(string Id, UpdateRecipeModel model)
        {

            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(UpdateRecipe), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "UpdateRecipe : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException(nameof(UpdateRecipe), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "UpdateRecipe : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.MODEL);
                }
                if (!this.CheckIsAdmin())
                {
                    if (!await _sender.Send(new CheckRecipeAccesQuery(guid, this.GetUserGuid())))
                        throw new ForbiddenAccessException(nameof(UpdateRecipe), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.FORBIDDEN, "UpdateRecipe : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.FORBIDDEN);
                }
                await _sender.Send(model.ToCommand(guid));
                _logger.LogInformation("UpdateRecipe : finish without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (ForbiddenAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ForbiddenAccessException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception();
            }
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteRecipe(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException(nameof(DeleteRecipe), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER, "DeleteRecipe : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }

                if (!this.CheckIsAdmin())
                {
                    if (!await _sender.Send(new CheckRecipeAccesQuery(guid, this.GetUserGuid())))
                        throw new ForbiddenAccessException(nameof(DeleteRecipe), Path.GetFileName("RecipeController"), Constant.EXCEPTION.TITLE.FORBIDDEN, "DeleteRecipe : " + Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.FORBIDDEN);
                }
                await _sender.Send(guid.ToDeleteRecipeCommand());
                _logger.LogInformation("UpdateRecipe : finish without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (ForbiddenAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ForbiddenAccessException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
