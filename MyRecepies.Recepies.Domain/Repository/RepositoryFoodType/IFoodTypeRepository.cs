using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryFoodType
{
    public interface IFoodTypeRepository : IRepository<FoodType, Guid>
    {
        public Task<bool> FoodTypeExist(string name);
    }
}
