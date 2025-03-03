using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipe
{
    public interface IRecipesRepository : IRepository<Recipe, Guid>
    {
        public Task<ICollection<Recipe>> GetByNameAsync(string Name);
        public Task<ICollection<Recipe>> GetByRecipeIdAsync(Guid recipeId);
    }
}
