using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient
{
    public class GetAllRecipeIngredientQuery : IRequest<List<GetAllRecipeIngredientQueryResult>>
    {
    }
}
