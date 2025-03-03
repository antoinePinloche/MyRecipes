using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryIngredient
{
    public interface IIngredientRepository : IRepository<Ingredient, Guid>
    {
        public Task<Ingredient> HasIngredient(string Name);
        public Task<List<Ingredient>> GetAllIngredientsByFoodTypeId(Guid foodTypeId);
    }
}
