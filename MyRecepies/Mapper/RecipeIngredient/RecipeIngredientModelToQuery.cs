using GetRIByID = MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using GetRIByRecipeID = MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;

namespace MyRecipes.Web.API.Mapper.RecipeIngredient
{
    public static class RecipeIngredientModelToQuery
    {
        public static GetRIByID.GetRecipeIngredientByIdQuery ToRecipeIngredientByIdQuery(this Guid id)
        {
            return new GetRIByID.GetRecipeIngredientByIdQuery(id);
        }

        public static GetRIByRecipeID.GetRecipeIngredientByRecipeIdQuery ToRecipeIngredientByRecipeIdQuery(this Guid id)
        {
            return new GetRIByRecipeID.GetRecipeIngredientByRecipeIdQuery(id);
        }
    }
}
