using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient, Guid>
    {
        /// <summary>
        /// retourne les RecipeIngredient d'une recette par son ID
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public abstract Task<ICollection<RecipeIngredient>> GetAllRecipeIngredientByRecipeIdlAsync(Guid Key);
    }
}
