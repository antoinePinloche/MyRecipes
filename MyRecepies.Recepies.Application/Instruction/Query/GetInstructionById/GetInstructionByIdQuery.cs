using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById
{
    public class GetInstructionByIdQuery : IRequest<GetInstructionByIdQueryResult>
    {
        public Guid Id { get; set; }
        public GetInstructionByIdQuery(Guid id) => Id = id;
    }
}
