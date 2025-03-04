using MyRecipes.Recipes.Domain.Entity;
namespace MyRecipes.Recipes.Domain.Repository.RepositoryFoodType
{
    /// <summary>
    /// Class abstraite repressentant la base des appels en DB pour l'entité FoodType
    /// <see cref="IFoodTypeRepository"/>
    /// </summary>
    public abstract class FoodTypeBase : IFoodTypeRepository
    {
        public abstract Task<FoodType> AddAsync(FoodType entity);
        public abstract Task<ICollection<FoodType>> AddRangeAsync(ICollection<FoodType> entities);
        public abstract FoodType FirstOrDefault(Func<FoodType, bool> predicate);
        public abstract Task<ICollection<FoodType>> GetAllAsync();
        public abstract Task<FoodType> GetAsync(Guid key);
        public abstract Task RemoveAsync(FoodType entitie);
        public abstract Task RemoveRangeAsync(ICollection<FoodType> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(FoodType entity);
        public abstract Task UpdateRangeAsync(ICollection<FoodType> entities);
        /// <summary>
        /// <see cref="IFoodTypeRepository.FoodTypeExist"/> 
        /// </summary>
        public abstract Task<bool> FoodTypeExist(string name);
    }
}
