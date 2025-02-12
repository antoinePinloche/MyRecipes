using MediatR;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    public class GetAllIngredientQueryhandler : IRequestHandler<GetAllIngredientQuery, GetAllIngredientQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        public GetAllIngredientQueryhandler(IIngredientRepository ingredienRepository) => _ingredienRepository = ingredienRepository;

        public async Task<GetAllIngredientQueryResult> Handle(GetAllIngredientQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entity.Ingredient> result = new List<Domain.Entity.Ingredient>();
            var entities = await _ingredienRepository.GetAllAsync();
            if (entities is not null)
            {
                foreach (var entity in entities)
                {
                    result.Add((Domain.Entity.Ingredient)entity);
                }
            }
            return new GetAllIngredientQueryResult(result);
        }
    }
}
