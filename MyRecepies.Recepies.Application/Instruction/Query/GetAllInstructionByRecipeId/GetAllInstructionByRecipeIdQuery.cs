using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    public class GetAllInstructionByRecipeIdQuery : IRequest<List<GetAllInstructionByRecipeIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetAllInstructionByRecipeIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
