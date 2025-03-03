using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe
{
    /// <summary>
    /// Command pour supprimer une recette
    /// <see cref="DeleteRecipeCommandHandler"/>
    /// </summary>
    public class DeleteRecipeCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRecipeCommand(Guid id) => Id = id;
    }
}
