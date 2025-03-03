using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.CheckInstructionAcces
{
    /// <summary>
    /// Query pour vérifier que l'utilisateur est le créateur de la recette lié a l'instruction que l'on souhaite modifier
    /// <see cref="CheckInstructionAccesQueryHandler"/>
    /// </summary>
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
