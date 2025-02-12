using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQueryHandler : IRequestHandler<GetIngredientsByFoodTypeIdQuery, GetIngredientsByFoodTypeIdQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        public GetIngredientsByFoodTypeIdQueryHandler(IIngredientRepository ingredienRepository) => _ingredienRepository = ingredienRepository;

        public async Task<GetIngredientsByFoodTypeIdQueryResult> Handle(GetIngredientsByFoodTypeIdQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entity.Ingredient> ingredients = await _ingredienRepository.GetAllIngredientsByFoodTypeId(request.Id);

            List<GetIngredientsByFoodTypeIdQueryResult.Ingredient> ingredientsToReturn = new List<GetIngredientsByFoodTypeIdQueryResult.Ingredient>();
            foreach (Domain.Entity.Ingredient ingredient in ingredients)
            {
                ingredientsToReturn.Add(new GetIngredientsByFoodTypeIdQueryResult.Ingredient(ingredient.Id, ingredient.Name, ingredient.FoodType.Name));
            }
            return new GetIngredientsByFoodTypeIdQueryResult(ingredientsToReturn);
        }
    }
}
