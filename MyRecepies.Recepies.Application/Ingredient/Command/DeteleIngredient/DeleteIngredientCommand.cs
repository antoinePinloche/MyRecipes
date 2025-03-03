using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient
{
    /// <summary>
    /// Command pour supprimer un Ingredient
    /// <see cref="DeleteIngredientCommandHandler"/>
    /// </summary>
    public class DeleteIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteIngredientCommand(Guid id) => Id = id;
    }
}
