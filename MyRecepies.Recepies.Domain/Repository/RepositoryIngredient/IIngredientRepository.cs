using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryIngredient
{
    public interface IIngredientRepository : IRepository<Ingredient, Guid>
    {
        /// <summary>
        /// retourne l'ingredient avec une recherche par nom
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Task<Ingredient> HasIngredient(string Name);
        /// <summary>
        /// retourne la liste de tous les Ingredient relier au FoodType par son ID
        /// </summary>
        /// <param name="foodTypeId"></param>
        /// <returns></returns>
        public Task<List<Ingredient>> GetAllIngredientsByFoodTypeId(Guid foodTypeId);
    }
}
