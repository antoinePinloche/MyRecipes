using GetRecipeById = MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using GetRecipeByName = MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName;

namespace MyRecipes.Web.API.Mapper.Recipe
{
    public static class RecipeModelToQuery
    {
        public static GetRecipeByName.GetRecipeByNameQuery ToRecipeByNameQuery(this string str)
        {
            return new GetRecipeByName.GetRecipeByNameQuery(str);
        }

        public static GetRecipeById.GetRecipeByIdQuery ToRecipeByIdQuery(this Guid id)
        {
            return new GetRecipeById.GetRecipeByIdQuery(id);
        }
    }
}
