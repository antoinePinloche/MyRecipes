﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQueryHandler : IRequestHandler<GetIngredientsByFoodTypeIdQuery, List<GetIngredientsByFoodTypeIdQueryResult>>
    {
        private readonly IIngredientRepository _ingredienRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ILogger<GetIngredientsByFoodTypeIdQueryHandler> _logger;
        public GetIngredientsByFoodTypeIdQueryHandler(IIngredientRepository ingredienRepository, IFoodTypeRepository foodTypeRepository, ILogger<GetIngredientsByFoodTypeIdQueryHandler> logger)
        {
            _ingredienRepository = ingredienRepository;
            _foodTypeRepository = foodTypeRepository;
            _logger = logger;
        }

        public async Task<List<GetIngredientsByFoodTypeIdQueryResult>> Handle(GetIngredientsByFoodTypeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                Domain.Entity.FoodType foodType = await _foodTypeRepository.GetAsync(request.Id);
                if (foodType is null)
                {
                    throw new FoodTypeNotFoundException("Invalide key", $"FoodType not Found with Id {request.Id}");
                }
                List<Domain.Entity.Ingredient> ingredients = await _ingredienRepository.GetAllIngredientsByFoodTypeId(request.Id);

                if (!ingredients.IsNullOrEmpty())
                {
                    _logger.LogInformation($"GetIngredientsByFoodTypeIdQueryHandler : Ingredient(s) for FoodType {foodType.Name} return");
                    return ingredients.Select(i =>
                        new GetIngredientsByFoodTypeIdQueryResult()
                        {
                            Id = i.Id,
                            FoodTypeName = i.FoodType.Name,
                            Name = i.FoodType.Name
                        }

                    ).ToList();
                }
                _logger.LogInformation($"GetIngredientsByFoodTypeIdQueryHandler : finish without error for FoodType {foodType.Name} but with no ingredient");
                return new List<GetIngredientsByFoodTypeIdQueryResult>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
