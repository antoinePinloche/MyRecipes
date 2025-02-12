using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
    public class GetAllInstructionQueryHandler : IRequestHandler<GetAllInstructionQuery, List<GetAllInstructionQueryResult>>
    {
        private readonly IInstructionRepository _instructionRepository;
        public GetAllInstructionQueryHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task<List<GetAllInstructionQueryResult>> Handle(GetAllInstructionQuery request, CancellationToken cancellationToken)
        {
            var res = await _instructionRepository.GetAllAsync();
            return res.Select(s =>
                new GetAllInstructionQueryResult(
                    s.Id,
                    s.Step,
                    s.StepName,
                    s.StepInstruction
                    )
            ).ToList();
        }
    }
}
