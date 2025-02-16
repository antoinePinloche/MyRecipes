using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    public class GetAllIngredientQuery : IRequest<List<GetAllIngredientQueryResult>>
    {
        public GetAllIngredientQuery() { }
    }
}
