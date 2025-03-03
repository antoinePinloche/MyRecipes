using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    /// <summary>
    /// Command pour crée un FoodType
    /// <see cref="CreateFoodTypeCommandHandler"/>
    /// </summary>
    public class CreateFoodTypeCommand : IRequest
    {
        public string Name { get; set; }

        public CreateFoodTypeCommand(string name) => Name = name;
    }
}
