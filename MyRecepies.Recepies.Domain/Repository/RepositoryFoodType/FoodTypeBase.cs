using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryFoodType
{
    public abstract class FoodTypeBase : IFoodTypeRepository
    {
        public abstract Task<FoodType> AddAsync(FoodType entity);
        public abstract Task<FoodType> AddRangeAsync(ICollection<FoodType> entities);
        public abstract Task CreateOrUpdateSchemaAsync();
        public abstract FoodType FirstOrDefault(Func<FoodType, bool> predicate);

        public abstract bool FoodTypeByName(string name);

        public abstract Task<ICollection<FoodType>> GetAllAsync();
        public abstract Task<FoodType> GetAsync(Guid key);
        public abstract Task RemoveAsync(FoodType entitie);
        public abstract Task RemoveRangeAsync(ICollection<FoodType> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(FoodType entity);
        public abstract Task UpdateRangeAsync(ICollection<FoodType> entities);
    }
}
