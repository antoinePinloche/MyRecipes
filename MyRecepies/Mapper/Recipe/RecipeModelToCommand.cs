using MyRecipes.Web.API.Models.Class.Ingredient;
using MyRecipes.Web.API.Models.Class.Recipe.Model;
using Create = MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe;
using Delete = MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe;
using Update = MyRecipes.Recipes.Application.Recipe.Command.UpdateRecipe;

namespace MyRecipes.Web.API.Mapper.Recipe
{
    public static class RecipeModelToCommand
    {
        public static Create.CreateRecipeCommand ToCommand(this CreateRecipeModel model, Guid userGuid)
        {
            return new Create.CreateRecipeCommand(model.Name, model.RecipyDifficulty, model.TimeToPrepareRecipe, model.NbGuest, userGuid);
        }

        public static Delete.DeleteRecipeCommand ToDeleteRecipeCommand(this Guid id)
        {
            return new Delete.DeleteRecipeCommand(id);
        }

        public static Update.UpdateRecipeCommand ToCommand(this UpdateRecipeModel model, Guid id)
        {
            return new Update.UpdateRecipeCommand(id, model.Name, model.RecipyDifficulty, model.TimeToPrepareRecipe, model.NbGuest);
        }
    }
}
