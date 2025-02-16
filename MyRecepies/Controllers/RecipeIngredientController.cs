﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
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
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            try
            {
                var res = await _sender.Send(guid.ToRecipeIngredientByIdQuery());
                return Ok(res.ToRecipeIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpPost("api/[controller]/[action]")]
        public async Task<IResult> CreateRecipeIngredient(CreateRecipeIngredientModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            try
            {
                await _sender.Send(model.ToCommand());
                return Results.Created();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                throw new IngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientAlreadyExistException ex)
            {
                throw new RecipeIngredientAlreadyExistException(ex.Error, ex.Message);
            }

        }

        [HttpPut("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> UpdateRecipeIngredient(string Id, UpdateRecipeIngredientModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new WrongParameterException("Invalide parameter", "model is invalide");
            }
            if (!Guid.TryParse(Id, out Guid guid))
            {
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
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
            catch (RecipeIngredientNotFoundException ex)
            {
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientAlreadyExistException ex)
            {
                throw new RecipeIngredientAlreadyExistException(ex.Error, ex.Message);
            }
        }

        [HttpDelete("api/[controller]/[action]/{Id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            try
            {
                await _sender.Send(guid.ToDeleteRecipeIngredientCommand());
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
            
        }
    }
}
