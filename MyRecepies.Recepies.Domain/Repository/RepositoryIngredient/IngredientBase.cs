using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryIngredient
{
    public abstract class IngredientBase : IIngredientRepository
    {
        public abstract Task<Ingredient> AddAsync(Ingredient entity);
        public abstract Task<ICollection<Ingredient>> AddRangeAsync(ICollection<Ingredient> entities);
        public abstract Ingredient FirstOrDefault(Func<Ingredient, bool> predicate);
        public abstract Task<ICollection<Ingredient>> GetAllAsync();
        public abstract Task<Ingredient> GetAsync(Guid key);
        public abstract Task RemoveAsync(Ingredient entitie);
        public abstract Task RemoveRangeAsync(ICollection<Ingredient> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(Ingredient entity);
        public abstract Task UpdateRangeAsync(ICollection<Ingredient> entities);
        public abstract Task CreateOrUpdateSchemaAsync();
        /// <summary>
        /// <see cref="IIngredientRepository.HasIngredient"/> 
        /// </summary>
        public abstract Task<Ingredient> HasIngredient(string Name);
        /// <summary>
        /// <see cref="IIngredientRepository.GetAllIngredientsByFoodTypeId"/> 
        /// </summary>
        public abstract Task<List<Ingredient>> GetAllIngredientsByFoodTypeId(Guid foodTypeId);
    }
}
