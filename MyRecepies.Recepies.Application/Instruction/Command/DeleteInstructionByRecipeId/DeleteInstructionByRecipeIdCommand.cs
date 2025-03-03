using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId
{
    /// <summary>
    /// Command pour supprimer les instructions d'une recette par son RecipeID
    /// <see cref="DeleteInstructionByRecipeIdCommandHandler"/>
    /// </summary>
    public class DeleteInstructionByRecipeIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteInstructionByRecipeIdCommand(Guid id) => Id = id;
    }
}
