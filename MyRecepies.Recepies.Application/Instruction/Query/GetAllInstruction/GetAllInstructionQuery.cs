using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
    public class GetAllInstructionQuery : IRequest<List<GetAllInstructionQueryResult>>
    {

    }
}
