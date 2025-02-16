using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQuery : IRequest<List<GetIngredientsByFoodTypeIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetIngredientsByFoodTypeIdQuery(Guid id) => Id = id;
    }
}
