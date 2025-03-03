using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipe
{
    public abstract class RecipesBase : IRecipesRepository
    {
        public abstract Task<Recipe> AddAsync(Recipe entity);
        public abstract Task<ICollection<Recipe>> AddRangeAsync(ICollection<Recipe> entities);
        public abstract Recipe FirstOrDefault(Func<Recipe, bool> predicate);
        public abstract Task<ICollection<Recipe>> GetAllAsync();
        public abstract Task<Recipe> GetAsync(Guid key);
        public abstract Task RemoveAsync(Recipe entitie);
        public abstract Task RemoveRangeAsync(ICollection<Recipe> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(Recipe entity);
        public abstract Task UpdateRangeAsync(ICollection<Recipe> entities);
        public abstract Task CreateOrUpdateSchemaAsync();
        /// <summary>
        /// <see cref="IRecipesRepository.GetByRecipeByUserIdAsync"/>
        /// </summary>
        public abstract Task<ICollection<Recipe>> GetByRecipeByUserIdAsync(Guid userId);
        /// <summary>
        /// <see cref="IRecipesRepository.GetByNameAsync"/>
        /// </summary>
        public abstract Task<ICollection<Recipe>> GetByNameAsync(string Name);
    }
}
