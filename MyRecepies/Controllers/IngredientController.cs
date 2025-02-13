using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient;
using MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient;
using MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
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
            var result = await _sender.Send(new GetAllIngredientQuery());
            GetAllIngredientResponse response = new GetAllIngredientResponse();
            foreach (var item in result.ingredients)
            {
                response.Ingredients.Add(new GetAllIngredientResponse.Ingredient(item.Id, item.Name, item.FoodType.Name));
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Guid}")]
        public async Task<IActionResult> GetIngredient(string guid)
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetIngredientsByFoodType(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    return BadRequest("DeleteUser : BadParameter" + Id);
                }
                var result = await _sender.Send(new GetIngredientsByFoodTypeIdQuery(guid));
                GetAllIngredientResponse response = new GetAllIngredientResponse();
                foreach (var item in result.Ingredients)
                {
                    response.Ingredients.Add(new GetAllIngredientResponse.Ingredient(item.Id, item.Name, item.FoodTypeName));
                }
                return Ok(response);
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
                    return BadRequest("DeleteUser : BadParameter" + id);
                }
                await _sender.Send(new DeleteIngredientCommand(guid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
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
                return BadRequest(ModelState);
            try
            {
                await _sender.Send(new CreateIngredientCommand(ingredient.Name, ingredient.IngredientCategoryId));
            }
            catch(Exception ex)
            {
                throw new Exception("");
            }
            return Created();
        }
    }
}
