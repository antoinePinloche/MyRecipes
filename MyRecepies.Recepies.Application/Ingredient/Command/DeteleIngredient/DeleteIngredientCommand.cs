using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient
{
    public class DeleteIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteIngredientCommand(Guid id) => Id = id;
    }
}
