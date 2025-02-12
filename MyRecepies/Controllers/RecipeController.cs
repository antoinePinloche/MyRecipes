﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe;
using MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe;
using MyRecipes.Recipes.Application.Recipe.Command.UpdateRecipe;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Models.Class.Recipe;

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
            try
            {
                return Ok(await _sender.Send(new GetAllRecipeQuery()));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> GetRecipeById(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    return BadRequest("GetRecipeById : BadParameter" + Id);
                }
                return Ok(await _sender.Send(new GetRecipeByIdQuery(guid)));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("api/[controller]/{Id}/RecipeIngredient")]
        public async Task<IActionResult> GetRecipeIngredientByRecipeId(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    return BadRequest("GetRecipeById : BadParameter" + Id);
                }
                return Ok(await _sender.Send(new GetRecipeIngredientByRecipeIdQuery(guid)));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("api/[controller]/{Id}/Instructions")]
        public async Task<IActionResult> GetInstructionByRecipeId(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    return BadRequest("GetRecipeById : BadParameter" + Id);
                }
                return Ok(await _sender.Send(new GetAllInstructionByRecipeIdQuery(guid)));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("api/[controller]/[action]/{Name}")]
        public async Task<IActionResult> GetRecipeByName(string Name)
        {
            try
            {
                if (Name.IsNullOrEmpty() && Name.Count() < 3)
                    throw new Exception();
                return Ok(await _sender.Send(new GetRecipeByNameQuery(Name)));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> CreateRecipe(CreateRecipeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _sender.Send(new CreateRecipeCommand(model.Name, model.RecipyDifficulty, model.TimeToPrepareRecipe, model.NbGuest));
                return Created();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPut("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> UpdateRecipe(string Id, UpdateRecipeModel model)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("UpdateRecipe : BadParameter" + Id);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _sender.Send(new UpdateRecipeCommand(guid, model.Name, model.RecipyDifficulty, model.TimeToPrepareRecipe, model.NbGuest));
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpDelete("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> DeleteRecipe(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                return BadRequest("DeleteRecipe : BadParameter" + Id);
            }
            try
            {
                await _sender.Send(new DeleteRecipeCommand(guid));
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
