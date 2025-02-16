using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId
{
    public class DeleteInstructionByRecipeIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteInstructionByRecipeIdCommand(Guid id) => Id = id;
    }
}
