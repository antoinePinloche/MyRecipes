using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.CheckInstructionAcces
{
    public class CheckInstructionAccesQuery : IRequest<bool>
    {
        public Guid InstructionId { get; set; }
        public Guid UserId { get; set; }
        public CheckInstructionAccesQuery(Guid instructionId, Guid userId)
        {
            InstructionId = instructionId;
            UserId = userId;
        }
    }
}
