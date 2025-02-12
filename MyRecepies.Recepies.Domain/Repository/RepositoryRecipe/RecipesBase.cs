using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryRecipe
{
    public abstract class RecipesBase : IRecipesRepository
    {
        public abstract Task<Recipe> AddAsync(Recipe entity);
        public abstract Task<List<Recipe>> AddRangeAsync(ICollection<Recipe> entities);
        public abstract Recipe FirstOrDefault(Func<Recipe, bool> predicate);
        public abstract Task<ICollection<Recipe>> GetAllAsync();
        public abstract Task<ICollection<Recipe>> GetByNameAsync(string Name);
        public abstract Task<Recipe> GetAsync(Guid key);
        public abstract Task RemoveAsync(Recipe entitie);
        public abstract Task RemoveRangeAsync(ICollection<Recipe> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(Recipe entity);
        public abstract Task UpdateRangeAsync(ICollection<Recipe> entities);
        public abstract Task CreateOrUpdateSchemaAsync();

        Task<ICollection<Instruction>> IRepository<Recipe, Guid>.AddRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }
    }
}
