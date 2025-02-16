using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
using MyRecipes.Transverse.Exception;
using MyRecipes.Web.API.Mapper.Ingredient;
using MyRecipes.Web.API.Models.Class.Ingredient;

namespace MyRecipes.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly ISender _sender;

        public IngredientController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetIngredientList()
        {
            List<GetAllIngredientQueryResult> result = await _sender.Send(new GetAllIngredientQuery());
            return Ok(result.ToIngredientResponse());
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetIngredient(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                GetIngredientByIdQueryResult result = await _sender.Send(guid.ToQuery());
                return Ok(result.ToIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                throw new IngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetIngredientsByFoodType(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                List<GetIngredientsByFoodTypeIdQueryResult> result = await _sender.Send(guid.FoodTypeToQuery());
                return Ok(result.ToIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (FoodTypeNotFoundException ex)
            {
                throw new FoodTypeNotFoundException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                throw new IngredientAlreadyExistException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/[action]/{id}")]
        public async Task<IActionResult> DeleteIngredient(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                await _sender.Send(guid.ToDeleteIngredientCommand());
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                throw new IngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/[controller]/[action]/{Guid}")]
        public async Task<IActionResult> UpdateIngredient(string guid)
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateIngredient(CreateIngredientModel ingredient)
        {
            if (!ModelState.IsValid)
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            try
            {
                await _sender.Send(ingredient.ToCommand());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientAlreadyExistException ex)
            {
                throw new IngredientAlreadyExistException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
            return Created();
        }
    }
}
