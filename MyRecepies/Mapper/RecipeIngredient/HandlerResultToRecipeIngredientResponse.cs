using MyRecipes.Web.API.Models.Class.Ingredient;
using MyRecipes.Web.API.Models.Class.RecipeIngredient;
using RecipeIngredientQuery = MyRecipes.Recipes.Application.RecipeIngredient.Query;

namespace MyRecipes.Web.API.Mapper.RecipeIngredient
{
    public static class HandlerResultToRecipeIngredientResponse
    {
        public static List<RecipeIngredientResponse> ToRecipeIngredientResponse(this List<RecipeIngredientQuery.GetRecipeIngredientByRecipeId.GetRecipeIngredientByRecipeIdQueryResult> recipeIngredient)
        {
            return recipeIngredient.Select(i =>
                new RecipeIngredientResponse()
                {
                    Id = i.Id,
                    Ingredient = new IngredientResponse(i.Ingredient.Id , i.Ingredient?.Name, i.Ingredient?.FoodType.Name),
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }
            ).ToList();
        }

        public static List<RecipeIngredientResponse> ToRecipeIngredientResponse(this List<RecipeIngredientQuery.GetAllRecipeIngredient.GetAllRecipeIngredientQueryResult> recipeIngredient)
        {
            return recipeIngredient.Select(i =>
                    new RecipeIngredientResponse()
                    {
                        Id = i.Id,
                        Ingredient = new IngredientResponse(i.Ingredient.Id, i.Ingredient?.Name, i.Ingredient?.FoodType.Name),
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    }
                ).ToList();
        }

        public static RecipeIngredientResponse ToRecipeIngredientResponse(this RecipeIngredientQuery.GetRecipeIngredientById.GetRecipeIngredientByIdQueryResult recipeIngredient)
        {
            return new RecipeIngredientResponse()
            {
                Id = recipeIngredient.Id,
                Ingredient = new IngredientResponse(recipeIngredient.Ingredient.Id, recipeIngredient.Ingredient?.Name, recipeIngredient.Ingredient?.FoodType.Name),
                Quantity = recipeIngredient.Quantity,
                Unit = recipeIngredient.Unit
            };
        }
    }
}
