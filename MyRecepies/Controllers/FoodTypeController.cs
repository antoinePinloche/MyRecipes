using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById;
using MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Models.Class.FoodType;
using System;

namespace MyRecipes.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public FoodTypeController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetAllFoodType()
        {
            var res = await _sender.Send(new GetAllFoodTypeQuery());
            return Ok(res);
        }

        [HttpPost]
        [Route("/[action]")]
        public async Task<IActionResult> CreateFoodType(CreateFoodTypeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new WrongParameterException(nameof(CreateFoodType), nameof(FoodTypeController), "Invalid model");
                }
                if (model.Name.IsNullOrEmpty())
                {
                    throw new WrongParameterException(nameof(CreateFoodType), nameof(CreateFoodTypeModel.Name), "Invalid name");
                }
                await _sender.Send(new CreateFoodTypeCommand(model.Name));
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/[action]/{id}")]
        public async Task<IActionResult> DeleteFoodType(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guid))
                {
                    return BadRequest("DeleteUser : BadParameter" + id);
                }
                var ingredientList = await _sender.Send(new GetIngredientsByFoodTypeIdQuery(guid));
                if (ingredientList.Ingredients.Any())
                {
                    return BadRequest("You can't Delete FoodType link to Ingredient(s). You need to delete them before.");
                }
                await _sender.Send(new DeleteFoodTypeByIdCommand(guid));
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("/[action]/{id}")]
        public async Task<IActionResult> UpdateFoodType(UpdateFoodTypeModel model,string id)
        {
            Guid guid;
            try
            {
                if (!Guid.TryParse(id, out guid))
                {
                    return BadRequest("DeleteUser : BadParameter" + id);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _sender.Send(new UpdateFoodTypeByIdCommand(guid, model.Name));
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
