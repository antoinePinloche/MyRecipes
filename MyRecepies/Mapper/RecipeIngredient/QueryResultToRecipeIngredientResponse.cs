using MyRecipes.Web.API.Models.Class.Ingredient;
using MyRecipes.Web.API.Models.Class.RecipeIngredient;
using RecipeIngredientQuery = MyRecipes.Recipes.Application.RecipeIngredient.Query;

namespace MyRecipes.Web.API.Mapper.RecipeIngredient
{
    public static class QueryResultToRecipeIngredientResponse
    {
        public static List<RecipeIngredientResponse> ToRecipeIngredientResponse(this List<RecipeIngredientQuery.GetRecipeIngredientByRecipeId.GetRecipeIngredientByRecipeIdQueryResult> recipeIngredient)
        {
            return recipeIngredient.Select(i =>
                new RecipeIngredientResponse()
                {
                    Id = i.Id,
                    Ingredient = new IngredientResponse(i.Ingredient?.Name, i.Ingredient?.FoodType.Name), //i.Ingredient
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }
            ).ToList();
        }
    }
}
