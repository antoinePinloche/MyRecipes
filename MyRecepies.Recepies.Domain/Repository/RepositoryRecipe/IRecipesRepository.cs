using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipe
{
    public interface IRecipesRepository : IRepository<Recipe, Guid>
    {
    }
}
