using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId
{
    /// <summary>
    /// Query pour retourner les RecipeIngredient d'une recette
    /// <see cref="GetRecipeIngredientByRecipeIdQueryHandler"/>
    /// </summary>
    public class GetRecipeIngredientByRecipeIdQuery : IRequest<List<GetRecipeIngredientByRecipeIdQueryResult>>
    {
        public Guid Id { get; set; }
        public GetRecipeIngredientByRecipeIdQuery(Guid id) => Id = id;
    }
}
