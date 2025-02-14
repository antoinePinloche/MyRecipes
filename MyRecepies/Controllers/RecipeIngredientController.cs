using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Transverse.Exception;
using MyRecipes.Web.API.Mapper.RecipeIngredient;
using MyRecipes.Web.API.Models.Class.RecipeIngredient;
using System;

namespace MyRecipes.Web.API.Controllers
{
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly ISender _sender;

        public RecipeIngredientController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpGet("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllRecipeIngredient()
        {
            var res = await _sender.Send(new GetAllRecipeIngredientQuery());
            return Ok(res.ToRecipeIngredientResponse());
        }

        [HttpGet("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetRecipeIngredient(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("DeleteUser : BadParameter" + Id);
            }
            var res = await _sender.Send(guid.ToRecipeIngredientByIdQuery());
            return Ok(res.ToRecipeIngredientResponse());
        }

        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> CreateRecipeIngredient(CreateRecipeIngredientModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Bad parameter");
            }
            await _sender.Send(model.ToCommand()); 
            return Ok();
        }

        [HttpPut("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> UpdateRecipeIngredient(string Id, UpdateRecipeIngredientModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Bad parameter");
            }
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("UpdateRecipeIngredient : BadParameter" + Id);
            }
            await _sender.Send(model.ToCommand(guid));
            return Ok();
        }

        [HttpDelete("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("DeleteRecipeIngredient : BadParameter" + Id);
            }
            await _sender.Send(guid.ToDeleteRecipeIngredientCommand());
            return Ok();
        }
    }
}
