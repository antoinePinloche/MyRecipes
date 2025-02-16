using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    public class GetAllIngredientQueryhandler : IRequestHandler<GetAllIngredientQuery, List<GetAllIngredientQueryResult>>
    {
        private readonly IIngredientRepository _ingredienRepository;
        private readonly ILogger<GetAllIngredientQueryhandler> _logger;
        public GetAllIngredientQueryhandler(IIngredientRepository ingredienRepository, ILogger<GetAllIngredientQueryhandler> logger)
        {
            _ingredienRepository = ingredienRepository;
            _logger = logger;
        }

        public async Task<List<GetAllIngredientQueryResult>> Handle(GetAllIngredientQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entity.Ingredient> result = new List<Domain.Entity.Ingredient>();
            var entities = await _ingredienRepository.GetAllAsync();
            if (entities is not null)
            {
                _logger.LogInformation($"GetAllIngredientQueryhandler : Ingredient found return");
                return entities.Select(e =>
                    new GetAllIngredientQueryResult()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        FoodTypeInformation = new GetAllIngredientQueryResult.FoodType()
                        {
                            Id = e.FoodType.Id,
                            Name = e.FoodType.Name
                        }
                    }

                ).ToList();
            }
            _logger.LogInformation($"GetAllIngredientQueryhandler : complete without error and no ingredient to return");
            return new List<GetAllIngredientQueryResult>();
        }
    }
}
