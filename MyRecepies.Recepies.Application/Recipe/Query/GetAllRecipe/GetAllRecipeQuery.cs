using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe
{
    /// <summary>
    /// Query pour retourner toutes les recettes
    /// <see cref="GetAllRecipeQueryHandler"/>
    /// </summary>
    public class GetAllRecipeQuery : IRequest<List<GetAllRecipeQueryResult>>
    {
    }
}
