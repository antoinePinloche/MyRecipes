using MyRecipes.Web.API.Models.Class.Ingredient;
using Create = MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient;
using Delete = MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient;

namespace MyRecipes.Web.API.Mapper.Ingredient
{
    public static class IngredientModelToCommand
    {
        public static Create.CreateIngredientCommand ToCommand(this CreateIngredientModel model)
        {
            return new Create.CreateIngredientCommand(model.Name, model.IngredientCategoryId);
        }

        public static Delete.DeleteIngredientCommand ToDeleteIngredientCommand(this Guid id)
        {
            return new Delete.DeleteIngredientCommand(id);
        }
    }
}
