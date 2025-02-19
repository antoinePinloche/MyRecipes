using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Recipe.Query.CheckRecipeAcces;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.CheckRecipeIngredientAcces;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Mapper.RecipeIngredient;
using MyRecipes.Web.API.Models.Class.RecipeIngredient;

namespace MyRecipes.Web.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Constant.ROLE.ADMINANDUSER)]
    [Route(Constant.CONTROLLER_ROUTE.RECIPE_INGREDIENT)]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<RecipeIngredientController> _logger;

        public RecipeIngredientController(ISender mediator, ILogger<RecipeIngredientController> logger)
        {
            _sender = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRecipeIngredient()
        {
            var res = await _sender.Send(new GetAllRecipeIngredientQuery());
            _logger.LogInformation("GetAllRecipeIngredient : finish without error");
            return Ok(res.ToRecipeIngredientResponse());
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetRecipeIngredient(string Id)
        {

            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                var res = await _sender.Send(guid.ToRecipeIngredientByIdQuery());
                _logger.LogInformation("GetRecipeIngredient : finish without error");
                return Ok(res.ToRecipeIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IResult> CreateRecipeIngredient(CreateRecipeIngredientModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException("Invalide parameter", "model is invalide");
                }
                await _sender.Send(model.ToCommand());
                _logger.LogInformation("CreateRecipeIngredient : finish without error");
                return Results.Created();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new IngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientAlreadyExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeIngredientAlreadyExistException(ex.Error, ex.Message);
            }

        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateRecipeIngredient(string Id, UpdateRecipeIngredientModel model)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException("Invalide parameter", "model is invalide");
                }
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                if (!this.CheckIsAdmin())
                {
                    if (!await _sender.Send(new CheckRecipeIngredientAccesQuery(guid, this.GetUserGuid())))
                        throw new Exception();
                }
                await _sender.Send(model.ToCommand(guid));
                _logger.LogInformation("UpdateRecipeIngredient : finish without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientAlreadyExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeIngredientAlreadyExistException(ex.Error, ex.Message);
            }
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(string Id)
        {

            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                if (!this.CheckIsAdmin())
                {
                    if (!await _sender.Send(new CheckRecipeIngredientAccesQuery(guid, this.GetUserGuid())))
                        throw new Exception();
                }
                await _sender.Send(guid.ToDeleteRecipeIngredientCommand());
                _logger.LogInformation("DeleteRecipeIngredient : finish without error");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
            
        }
    }
}
