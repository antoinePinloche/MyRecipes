using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient;
using MyRecipes.Web.API.Models.Class;

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

            return Ok();
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{Guid}")]
        public async Task<IActionResult> GetIngredient(string guid)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("api/[controller]/[action]/{Guid}")]
        public async Task<IActionResult> DeleteIngredient(string guid)
        {
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
            await _sender.Send(new CreateIngredientCommand(ingredient.Name, ingredient.IngredientCategoryId));
            return Created();
        }
    }
}
