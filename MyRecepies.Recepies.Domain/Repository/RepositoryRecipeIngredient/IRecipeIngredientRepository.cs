using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient, Guid>
    {
        public abstract Task<ICollection<RecipeIngredient>> GetAllRecipeIngredientByRecipeIdlAsync(Guid Key);
    }
}
