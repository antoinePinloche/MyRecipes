using MyRecipes.Web.API.Models.Class.RecipeIngredient.Model;
using Create = MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient;
using Delete = MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient;
using DeleteByRecipeId = MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId;
using Update = MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient;

namespace MyRecipes.Web.API.Mapper.RecipeIngredient
{
    public static class RecipeIngredientModelToCommand
    {
        public static Create.CreateRecipeIngredientCommand ToCommand(this CreateRecipeIngredientModel model)
        {
            return new Create.CreateRecipeIngredientCommand()
            {
                IngredientId = model.IngredientId,
                RecipeId = model.RecipeId,
                Quantity = model.Quantity,
                Unit = model.Unit
            };
        }

        public static Delete.DeleteRecipeIngredientCommand ToDeleteRecipeIngredientCommand(this Guid id)
        {
            return new Delete.DeleteRecipeIngredientCommand(id);
        }

        public static DeleteByRecipeId.DeleteRecipeIngredientByRecipeIdCommand ToDeleteByRecipeIdCommand(this Guid id)
        {
            return new DeleteByRecipeId.DeleteRecipeIngredientByRecipeIdCommand(id);
        }

        public static Update.UpdateRecipeIngredientCommand ToCommand(this UpdateRecipeIngredientModel model, Guid id)
        {
            return new Update.UpdateRecipeIngredientCommand(id, model.IngredientId, model.Quantity, model.Unit, model.RecipeId);
        }

    }
}
