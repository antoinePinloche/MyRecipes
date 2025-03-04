using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryFoodType
{
    /// <summary>
    /// Interface pour le DBContext FoodType
    /// <see cref="IRepository{TEntity, TKey}"/>
    /// </summary>
    public interface IFoodTypeRepository : IRepository<FoodType, Guid>
    {
        /// <summary>
        /// permet de savoir si le FoodType existe déja
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<bool> FoodTypeExist(string name);
    }
}
