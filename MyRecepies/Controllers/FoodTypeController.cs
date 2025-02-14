using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById;
using MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Mapper.FoodType;
using MyRecipes.Web.API.Mapper.Ingredient;
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
        public async Task<IResult> GetAllFoodType()
        {
            var res = await _sender.Send(new GetAllFoodTypeQuery());
            return Results.Ok(res);
        }

        [HttpGet]
        [Route("/[action]/{Id}")]
        public async Task<IResult> GetFoodType(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return Results.BadRequest("DeleteUser : BadParameter" + Id);
            }
            try
            {
                var res = await _sender.Send(new GetFoodTypeByIdQuery(guid));
                if (res is null || res.FoodType is null)
                    throw new FoodTypeNotFoundException("Not Found", $"FoodType with {Id} can't be found");
                return Results.Ok(res);
            }
            catch (Exception ex)
            {
                return Results.Problem();
             //   throw new ExceptionBase("Internal Error", ex.Message);
            }
            
        }

        [HttpPost]
        [Route("/[action]")]
        public async Task<IResult> CreateFoodType(CreateFoodTypeModel model)
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
                await _sender.Send(model.ToCreateFoodTypeCommand());
                return Results.Created();
            }
            catch (FoodTypeAlreadyExistException ex)
            {
                throw new FoodTypeAlreadyExistException(ex.Error, ex.Message);
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
                var ingredientList = await _sender.Send(guid.FoodTypeToQuery());
                if (ingredientList.Any())
                {
                    return BadRequest("You can't Delete FoodType link to Ingredient(s). You need to delete them before.");
                }
                await _sender.Send(guid.ToDeleteFoodTypeByIdCommand());
                return Ok();
            }
            catch (FoodTypeNotFoundException ex)
            {
                throw new FoodTypeNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpPut]
        [Route("/[action]/{id}")]
        public async Task<IResult> UpdateFoodType(UpdateFoodTypeModel model,string id)
        {
            Guid guid;
            try
            {
                if (!Guid.TryParse(id, out guid))
                {
                    return Results.BadRequest("DeleteUser : BadParameter" + id);
                }
                if (!ModelState.IsValid)
                    return Results.BadRequest(ModelState);
                await _sender.Send(model.ToUpdateFoodTypeByIdCommand(guid));
                return Results.Ok();
            }
            catch (FoodTypeAlreadyExistException ex)
            {
                throw new FoodTypeAlreadyExistException(ex.Error, ex.Message);
            }
            catch (FoodTypeNotFoundException ex)
            {
                throw new FoodTypeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem();
            }
        }
    }
}
