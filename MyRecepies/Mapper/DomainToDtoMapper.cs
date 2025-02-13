using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Web.API.Models.Class.Ingredient;

namespace MyRecipes.Web.API.Mapper
{
    public static class DomainToDtoMapper
    {
        public static IngredientResponse ToIngredientResponse(this Ingredient recipe)
        {
            return new IngredientResponse
            {
                DisplayName = recipe.Name,
                FoodTypeDisplayName = recipe.FoodType.Name
            };
        }
    }
}
