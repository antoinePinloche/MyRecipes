using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
    /// <summary>
    /// Query pour retourner toutes les instructions
    /// <see cref="GetAllInstructionQueryHandler"/>
    /// </summary>
    public class GetAllInstructionQuery : IRequest<List<GetAllInstructionQueryResult>>
    {

    }
}
