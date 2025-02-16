﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FoodTypeController> _logger;

        public FoodTypeController(ISender mediator, ILogger<FoodTypeController> logger)
        {
            _sender = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IResult> GetAllFoodType()
        {
            var res = await _sender.Send(new GetAllFoodTypeQuery());
            _logger.LogInformation("GetAllFoodType : finish without problem");
            return Results.Ok(res);
        }

        [HttpGet]
        [Route("/[action]/{Id}")]
        public async Task<IResult> GetFoodType(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guid))
            {
                _logger.LogError("GetFoodType : parameter ID is invalide");
                throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
            }
            try
            {
                var res = await _sender.Send(new GetFoodTypeByIdQuery(guid));
                if (res is null || res.FoodType is null)
                {
                    _logger.LogError($"GetFoodType : FoodType with {Id} can't be found");
                    throw new FoodTypeNotFoundException("Not Found", $"FoodType with {Id} can't be found");
                }
                _logger.LogInformation("GetFoodType : finish without problem");
                return Results.Ok(res);
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (FoodTypeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FoodTypeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Results.Problem();
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
                    _logger.LogError("CreateFoodType : model is invalide");
                    throw new WrongParameterException("Invalide parameter", "model invalide");
                }
                await _sender.Send(model.ToCreateFoodTypeCommand());
                _logger.LogInformation("CreateFoodType : finish without problem");
                return Results.Created();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (FoodTypeAlreadyExistException ex)
            {
                _logger.LogError(ex, ex.Message);
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
                    _logger.LogError("DeleteFoodType : parameter ID is invalide");
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                var ingredientList = await _sender.Send(guid.FoodTypeToQuery());
                if (ingredientList.Any())
                {
                    _logger.LogError($"DeleteFoodType : You can't Delete FoodType {id} link to Ingredient(s). You need to delete them before.");
                    return BadRequest("You can't Delete FoodType link to Ingredient(s). You need to delete them before.");
                }
                await _sender.Send(guid.ToDeleteFoodTypeByIdCommand());
                _logger.LogInformation("DeleteFoodType : finish without problem");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (FoodTypeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FoodTypeNotFoundException(ex.Error, ex.Message);
            }
        }

        [HttpPut]
        [Route("/[action]/{id}")]
        public async Task<IResult> UpdateFoodType(UpdateFoodTypeModel model,string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guid))
                {
                    _logger.LogError("UpdateFoodType : parameter ID is invalide)");
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("UpdateFoodType : model is invalide)");
                    throw new WrongParameterException("Invalide parameter", "model is invalide");
                }
                    
                await _sender.Send(model.ToUpdateFoodTypeByIdCommand(guid));
                _logger.LogInformation("UpdateFoodType : finish without problem");
                return Results.Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (FoodTypeAlreadyExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FoodTypeAlreadyExistException(ex.Error, ex.Message);
            }
            catch (FoodTypeNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FoodTypeNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Results.Problem();
            }
        }
    }
}
