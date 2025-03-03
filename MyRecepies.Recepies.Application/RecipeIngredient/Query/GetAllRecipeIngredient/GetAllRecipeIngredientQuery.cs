using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient
{
    /// <summary>
    /// Query pour retourner tous les RecipeIngredient
    /// <see cref="GetAllRecipeIngredientQueryHandler"/>
    /// </summary>
    public class GetAllRecipeIngredientQuery : IRequest<List<GetAllRecipeIngredientQueryResult>>
    {
    }
}
