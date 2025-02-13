using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById;
using MyRecipes.Web.API.Models.Class.FoodType;

namespace MyRecipes.Web.API.Mapper.FoodType
{
    public static class FoodTypeModelToCommand
    {
        public static CreateFoodTypeCommand ToCreateFoodTypeCommand(this CreateFoodTypeModel model)
        {
            return new CreateFoodTypeCommand(model.Name);
        }
        public static UpdateFoodTypeByIdCommand ToUpdateFoodTypeByIdCommand(this UpdateFoodTypeModel model, Guid id)
        {
            return new UpdateFoodTypeByIdCommand(id, model.Name);
        }
    }
}
