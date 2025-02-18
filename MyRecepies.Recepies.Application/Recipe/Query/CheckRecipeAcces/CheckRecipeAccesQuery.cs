using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.CheckRecipeAcces
{
    public class CheckRecipeAccesQuery : IRequest<bool>
    {
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }

        public CheckRecipeAccesQuery(Guid recipeId, Guid userId)
        {
            RecipeId = recipeId;
            UserId = userId;
        }
    }
}
