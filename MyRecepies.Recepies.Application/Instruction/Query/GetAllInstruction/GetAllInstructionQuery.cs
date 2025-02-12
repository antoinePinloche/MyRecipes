using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
    public class GetAllInstructionQuery : IRequest<List<GetAllInstructionQueryResult>>
    {

    }
}
