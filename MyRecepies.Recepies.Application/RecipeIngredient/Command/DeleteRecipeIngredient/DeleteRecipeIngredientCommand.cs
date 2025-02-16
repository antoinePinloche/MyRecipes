using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient
{
    public class DeleteRecipeIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteRecipeIngredientCommand(Guid id) => this.Id = id;
    }
}
