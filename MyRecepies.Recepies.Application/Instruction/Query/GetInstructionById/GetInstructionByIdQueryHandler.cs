using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById
{
    public class GetInstructionByIdQueryHandler : IRequestHandler<GetInstructionByIdQuery, GetInstructionByIdQueryResult>
    {
        private readonly IInstructionRepository _instructionRepository;
        public GetInstructionByIdQueryHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task<GetInstructionByIdQueryResult> Handle(GetInstructionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new Exception();
            if (request.Id.IsEmpty())
                throw new Exception();
            try
            {
                var entityFound = await _instructionRepository.GetAsync(request.Id);
                return new GetInstructionByIdQueryResult(entityFound.Id, entityFound.Step, entityFound.StepName, entityFound.StepInstruction);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            throw new NotImplementedException();
        }
    }
}
