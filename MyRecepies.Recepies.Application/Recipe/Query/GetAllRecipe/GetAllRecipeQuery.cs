using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe
{
    public class GetAllRecipeQuery : IRequest<List<GetAllRecipeQueryResult>>
    {
    }
}
