using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById
{
    public class UpdateFoodTypeByIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateFoodTypeByIdCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
