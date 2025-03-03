using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction
{
    /// <summary>
    /// Command pour supprimer une instruction par son ID
    /// <see cref="DeleteInstructionCommandHandler"/>
    /// </summary>
    public class DeleteInstructionCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteInstructionCommand(Guid id) => Id = id;
    }
}
