using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient
{
    public abstract class RecipeIngredientBase : IRecipeIngredientRepository
    {
        public abstract Task<RecipeIngredient> AddAsync(RecipeIngredient entity);
        public abstract Task<RecipeIngredient> AddRangeAsync(ICollection<RecipeIngredient> entities);
        public abstract RecipeIngredient FirstOrDefault(Func<RecipeIngredient, bool> predicate);
        public abstract Task<ICollection<RecipeIngredient>> GetAllAsync();
        public abstract Task<RecipeIngredient> GetAsync(Guid key);
        public abstract Task RemoveAsync(RecipeIngredient entitie);
        public abstract Task RemoveRangeAsync(ICollection<RecipeIngredient> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(RecipeIngredient entity);
        public abstract Task UpdateRangeAsync(ICollection<RecipeIngredient> entities);
        public abstract Task CreateOrUpdateSchemaAsync();
        public abstract Task<ICollection<RecipeIngredient>> GetAllRecipeIngredientByRecipeIdlAsync(Guid Key);

        Task<ICollection<Instruction>> IRepository<RecipeIngredient, Guid>.AddRangeAsync(ICollection<RecipeIngredient> entities)
        {
            throw new NotImplementedException();
        }
    }
}
