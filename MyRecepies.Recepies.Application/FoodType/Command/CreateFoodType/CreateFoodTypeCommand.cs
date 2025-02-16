using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    public class CreateFoodTypeCommand : IRequest
    {
        public string Name { get; set; }

        public CreateFoodTypeCommand(string name) => Name = name;
    }
}
