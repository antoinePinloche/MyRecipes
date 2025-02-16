using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe
{
    public class DeleteRecipeCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRecipeCommand(Guid id) => Id = id;
    }
}
