using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, GetIngredientByIdQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        public GetIngredientByIdQueryHandler(IIngredientRepository ingredienRepository) => _ingredienRepository = ingredienRepository;

        public async Task<GetIngredientByIdQueryResult> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entity.Ingredient ingredient = await _ingredienRepository.GetAsync(request.Id);
            return new GetIngredientByIdQueryResult()
            {
                FoodTypeName = "ingredient.FoodType.Name",
                IngredientFound = new GetIngredientByIdQueryResult.Ingredient(ingredient.Id, ingredient.Name, "ingredient.FoodType.Name")
            };
        }
    }
}
