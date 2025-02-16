using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction
{
    public class DeleteInstructionCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteInstructionCommand(Guid id) => Id = id;
    }
}
