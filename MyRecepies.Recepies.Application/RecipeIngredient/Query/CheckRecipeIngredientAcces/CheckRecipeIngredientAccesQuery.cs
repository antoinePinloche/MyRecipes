using MediatR;
using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.CheckRecipeIngredientAcces
{
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
