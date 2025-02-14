using MyRecipes.Web.API.Models.Class.Ingredient;
using IgAll = MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient;
using IgbyFoodType = MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
using IgById = MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;

namespace MyRecipes.Web.API.Mapper.Ingredient
{
    public static class HandlerResultToIngredientResponse
    {
        public static List<DetailIngredientResponse> ToIngredientResponse(this List<IgAll.GetAllIngredientQueryResult> ingredients)
        {
            return ingredients.Select(i =>
                new DetailIngredientResponse()
                {
                    Id = i.Id,
                    DisplayName = i.Name,
                    FoodTypeInformation = new DetailIngredientResponse.FoodType()
                    {
                        Id = i.FoodTypeInformation.Id,
                        Name = i.FoodTypeInformation.Name,
                    }
                }
            ).ToList();
        }

        public static List<IngredientResponse> ToIngredientResponse(this List<IgbyFoodType.GetIngredientsByFoodTypeIdQueryResult> ingredients)
        {
            return ingredients.Select(i =>
                new IngredientResponse()
                {
                    Id = i.Id,
                    DisplayName = i.Name,
                    FoodTypeDisplayName = i.FoodTypeName
                }
            ).ToList();
        }

        public static IngredientResponse ToIngredientResponse(this IgById.GetIngredientByIdQueryResult ingredients)
        {
            return new IngredientResponse()
                {
                    Id = ingredients.Id,
                    DisplayName = ingredients.Name,
                    FoodTypeDisplayName = ingredients.FoodTypeName
                };
        }
    }
}
