using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient
{
    /// <summary>
    /// Class abstraite repressentant la base des appels en DB pour l'entité RecipeIngredient
    /// <see cref="IRecipeIngredientRepository"/>
    /// </summary>
    public abstract class RecipeIngredientBase : IRecipeIngredientRepository
    {
        public abstract Task<RecipeIngredient> AddAsync(RecipeIngredient entity);
        public abstract Task<ICollection<RecipeIngredient>> AddRangeAsync(ICollection<RecipeIngredient> entities);
        public abstract RecipeIngredient FirstOrDefault(Func<RecipeIngredient, bool> predicate);
        public abstract Task<ICollection<RecipeIngredient>> GetAllAsync();
        public abstract Task<RecipeIngredient> GetAsync(Guid key);
        public abstract Task RemoveAsync(RecipeIngredient entitie);
        public abstract Task RemoveRangeAsync(ICollection<RecipeIngredient> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(RecipeIngredient entity);
        public abstract Task UpdateRangeAsync(ICollection<RecipeIngredient> entities);
        /// <summary>
        /// <see cref="IRecipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync"/>
        /// </summary>
        public abstract Task<ICollection<RecipeIngredient>> GetAllRecipeIngredientByRecipeIdlAsync(Guid Key);
    }
}
