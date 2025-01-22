using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Web.API.Models.Class;
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
            Guid guid;
            try
            {
                if (!Guid.TryParse(id, out guid))
                {
                    return BadRequest("DeleteUser : BadParameter" + id);
                }
                await _sender.Send(new DeleteFoodTypeByIdCommand(guid));
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
