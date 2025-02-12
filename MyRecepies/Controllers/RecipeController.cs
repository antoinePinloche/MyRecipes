using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;

namespace MyRecipes.Web.API.Controllers
{
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ISender _sender;

        public RecipeController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpGet("api/[controller]/[action]")]
        public async Task<IActionResult> GetAllRecipe()
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetRecipeById(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("GetRecipeById : BadParameter" + Id);
            }
            throw new NotImplementedException();
        }

        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> CreateRecipe()
        {
            throw new NotImplementedException();
        }

        [HttpPut("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> UpdateRecipe(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("UpdateRecipe : BadParameter" + Id);
            }
            throw new NotImplementedException();
        }

        [HttpDelete("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> DeleteRecipe(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("DeleteRecipe : BadParameter" + Id);
            }
            throw new NotImplementedException();
        }
    }
}
