using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient
{
    /// <summary>
    /// Command pour supprimer une RecipeIngredient par son ID
    /// <see cref="DeleteRecipeIngredientCommandHandler"/>
    /// </summary>
    public class DeleteRecipeIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteRecipeIngredientCommand(Guid id) => this.Id = id;
    }
}
