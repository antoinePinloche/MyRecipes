using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipe
{
    /// <summary>
    /// Interface pour le DBContext Recipes
    /// <see cref="IRepository{TEntity, TKey}"/>
    /// </summary>
    public interface IRecipesRepository : IRepository<Recipe, Guid>
    {
        /// <summary>
        /// retourne toutes les recette qui possede dans leur Name la string
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Task<ICollection<Recipe>> GetByNameAsync(string Name);
        /// <summary>
        /// retourne les recettes d'un utilisateur
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public Task<ICollection<Recipe>> GetByRecipeByUserIdAsync(Guid userId);
    }
}
