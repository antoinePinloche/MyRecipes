﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using MyRecipes.Web.API.Mapper.Ingredient;
using MyRecipes.Web.API.Models.Class.Ingredient;

namespace MyRecipes.web.Controllers
{
    [ApiController]
    [Authorize(Roles = Constant.ROLE.ADMINANDUSER)]
    [Route(Constant.CONTROLLER_ROUTE.INGREDIENT)]
    public class IngredientController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<IngredientController> _logger;

        public IngredientController(ISender mediator, ILogger<IngredientController> logger)
        {
            _sender = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetIngredientList()
        {
            List<GetAllIngredientQueryResult> result = await _sender.Send(new GetAllIngredientQuery());
            _logger.LogInformation("GetIngredientList : finish without problem");
            return Ok(result.ToIngredientResponse());
        }

        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetIngredient(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                GetIngredientByIdQueryResult result = await _sender.Send(guid.ToQuery());
                _logger.LogInformation("GetIngredient : finish without problem");
                return Ok(result.ToIngredientResponse());
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new IngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> GetIngredientsByFoodType(string Id)
        {
            try
            {
                if (!Guid.TryParse(Id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                List<GetIngredientsByFoodTypeIdQueryResult> result = await _sender.Send(guid.FoodTypeToQuery());
                _logger.LogInformation("GetIngredientsByFoodType : finish without problem");
                return Ok(result.ToIngredientResponse());
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
            catch (IngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new IngredientAlreadyExistException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteIngredient(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guid))
                {
                    throw new WrongParameterException("Invalide parameter", "parameter ID is invalide");
                }
                if (this.CheckIsAdmin())
                {
                    //check car user
                }
                await _sender.Send(guid.ToDeleteIngredientCommand());
                _logger.LogInformation("DeleteIngredient : finish without problem");
                return Ok();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new IngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("[action]/{Guid}")]
        public async Task<IActionResult> UpdateIngredient(string guid)
        {
            if (this.CheckIsAdmin())
            {
                //check car user
            }
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateIngredient(CreateIngredientModel ingredient)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new WrongParameterException("Invalide parameter", "model is invalide");
                await _sender.Send(ingredient.ToCommand());
                _logger.LogInformation("DeleteIngredient : finish without problem");
                return Created();
            }
            catch (WrongParameterException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new WrongParameterException(ex.Error, ex.Message);
            }
            catch (IngredientAlreadyExistException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new IngredientAlreadyExistException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception("");
            }
        }
    }
}
