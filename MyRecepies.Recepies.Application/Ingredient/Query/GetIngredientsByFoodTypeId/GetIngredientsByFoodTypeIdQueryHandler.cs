using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQueryHandler : IRequestHandler<GetIngredientsByFoodTypeIdQuery, List<GetIngredientsByFoodTypeIdQueryResult>>
    {
        private readonly IIngredientRepository _ingredienRepository;
        public GetIngredientsByFoodTypeIdQueryHandler(IIngredientRepository ingredienRepository) => _ingredienRepository = ingredienRepository;

        public async Task<List<GetIngredientsByFoodTypeIdQueryResult>> Handle(GetIngredientsByFoodTypeIdQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entity.Ingredient> ingredients = await _ingredienRepository.GetAllIngredientsByFoodTypeId(request.Id);

            if (!ingredients.IsNullOrEmpty())
            {
                return ingredients.Select(i =>
                    new GetIngredientsByFoodTypeIdQueryResult()
                    {
                        Id = i.Id,
                        FoodTypeName = i.FoodType.Name,
                        Name = i.FoodType.Name
                    }

                ).ToList();
            }
            return new List<GetIngredientsByFoodTypeIdQueryResult>();
        }
    }
}
