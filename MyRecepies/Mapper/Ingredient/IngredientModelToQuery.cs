using GetIngredientById = MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using IngredientsByFoodTypeId = MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;


namespace MyRecipes.Web.API.Mapper.Ingredient
{
    public static class IngredientModelToQuery
    {
        public static GetIngredientById.GetIngredientByIdQuery ToQuery(this Guid id)
        {
            return new GetIngredientById.GetIngredientByIdQuery(id);
        }

        public static IngredientsByFoodTypeId.GetIngredientsByFoodTypeIdQuery FoodTypeToQuery(this Guid id)
        {
            return new IngredientsByFoodTypeId.GetIngredientsByFoodTypeIdQuery(id);
        }
    }
}
