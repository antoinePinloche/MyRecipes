using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId
{
    public class DeleteRecipeIngredientByRecipeIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRecipeIngredientByRecipeIdCommand(Guid id) => Id = id;
    }
}
