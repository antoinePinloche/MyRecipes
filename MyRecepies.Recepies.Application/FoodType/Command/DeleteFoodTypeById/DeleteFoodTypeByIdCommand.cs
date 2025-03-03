using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById
{
    /// <summary>
    /// Command pour supprimer un FoodType
    /// <see cref="DeleteFoodTypeByIdCommandHandler"/>
    /// </summary>
    public class DeleteFoodTypeByIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteFoodTypeByIdCommand(Guid id) => Id = id;

    }
}
