using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById
{
    public class DeleteFoodTypeByIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteFoodTypeByIdCommand(Guid id) => Id = id;

    }
}
