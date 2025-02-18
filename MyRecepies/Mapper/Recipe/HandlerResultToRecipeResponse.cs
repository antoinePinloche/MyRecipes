using MyRecipes.Web.API.Models.Class.Recipe.Response;

namespace MyRecipes.Web.API.Mapper.Recipe
{
    public static class HandlerResultToRecipeResponse
    {
        public static DetailRecipeResponse ToRecipeResponse(this MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById.GetRecipeByIdQueryResult recipe)
        {
            return new DetailRecipeResponse()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Ingredients = recipe.Ingredients?.ToDetailRecipeResponseRecipeIngredient(),
                Instructions = recipe.Instructions?.ToDetailRecipeResponseInstruction(),
                RecipyDifficulty = recipe.RecipyDifficulty,
                TimeToPrepareRecipe = recipe.TimeToPrepareRecipe,
                NbGuest = recipe.NbGuest,
            };
        }

        public static List<RecipeResponse> ToRecipeResponse(this List<MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe.GetAllRecipeQueryResult> recipes)
        {
            return recipes.Select(r =>

                new RecipeResponse()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Ingredients = r.Ingredients?.ToRecipeResponseIngredient(),
                    Instructions = r.Instructions?.ToRecipeResponseInstruction(),
                    RecipyDifficulty = r.RecipyDifficulty,
                    TimeToPrepareRecipe = r.TimeToPrepareRecipe,
                    NbGuest = r.NbGuest,
                }).ToList();
        }

        public static List<RecipeResponse> ToRecipeResponse(this List<MyRecipes.Recipes.Application.Recipe.Query.GetMyRecipe.GetMyRecipeQueryResult> recipes)
        {
            return recipes.Select(r =>

                new RecipeResponse()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Ingredients = r.Ingredients?.ToRecipeResponseIngredient(),
                    Instructions = r.Instructions?.ToRecipeResponseInstruction(),
                    RecipyDifficulty = r.RecipyDifficulty,
                    TimeToPrepareRecipe = r.TimeToPrepareRecipe,
                    NbGuest = r.NbGuest,
                }).ToList();
        }
        

        public static List<RecipeByNameReponse> ToRecipeResponse(this List<Recipes.Application.Recipe.Query.GetRecipeByName.GetRecipeByNameQueryResult> recipes)
        {
            return recipes.Select(r =>

                new RecipeByNameReponse()
                {
                    Id = r.Id,
                    Name = r.Name,
                    RecipyDifficulty = r.RecipyDifficulty,
                    TimeToPrepareRecipe = r.TimeToPrepareRecipe,
                    NbGuest = r.NbGuest,
                }).ToList();
        }

    }
}
