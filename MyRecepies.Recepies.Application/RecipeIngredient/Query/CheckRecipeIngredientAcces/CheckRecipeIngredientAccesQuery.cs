using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.CheckRecipeIngredientAcces
{
    /// <summary>
    /// Query pour vérifier que l'utilisateur est le créateur de la recette
    /// <see cref="CheckRecipeIngredientAccesQueryHandler"/>
    /// </summary>
    public class CheckRecipeIngredientAccesQuery : IRequest<bool>
    {
        public Guid RecipeIngredientId { get; set; }
        public Guid UserId { get; set; }
        public CheckRecipeIngredientAccesQuery(Guid recipeIngredientId, Guid userId)
        {
            RecipeIngredientId = recipeIngredientId;
            UserId = userId;
        }
    }
}
