﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Mapper.Instruction;
using MyRecipes.Web.API.Mapper.Recipe;
using MyRecipes.Web.API.Mapper.RecipeIngredient;
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
                var result = await _sender.Send(new GetAllRecipeQuery());
                return Ok(result.ToRecipeResponse());
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
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                var result = await _sender.Send(guid.ToRecipeByIdQuery());
                    
                return Ok(result.ToRecipeResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException(ex.Error, ex.Message);
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
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                List<GetRecipeIngredientByRecipeIdQueryResult> result = await _sender.Send(guid.ToRecipeIngredientByRecipeIdQuery());
                
                return Ok(result.ToRecipeIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
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
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                List<GetAllInstructionByRecipeIdQueryResult> result = await _sender.Send(guid.ToAllInstructionByRecipeIdQuery());
                return Ok(result.ToInstructionResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (InstructionNotFoundException ex)
            {
                throw new InstructionNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpGet("api/[controller]/[action]/{Name}")]
        public async Task<IActionResult> GetRecipeByName(string Name)
        {
            try
            {
                if (Name.IsNullOrEmpty() && Name.Count() < 3)
                    throw new WrongParameterException("Invalide parameter", "parameter Nale is too short or missing");
                var result = await _sender.Send(Name.ToRecipeByNameQuery());
                return Ok(result.ToRecipeResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException(ex.Error, ex.Message);
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
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            try
            {
                await _sender.Send(model.ToCommand());
                return Created();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
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
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            if (!ModelState.IsValid)
            {
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            try
            {
                await _sender.Send(model.ToCommand(guid));
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException(ex.Error, ex.Message);
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
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            try
            {
                await _sender.Send(guid.ToDeleteRecipeCommand());
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
