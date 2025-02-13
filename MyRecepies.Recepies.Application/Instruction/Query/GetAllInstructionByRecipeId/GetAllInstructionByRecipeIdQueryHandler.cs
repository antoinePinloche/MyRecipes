using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    public class GetAllInstructionByRecipeIdQueryHandler : IRequestHandler<GetAllInstructionByRecipeIdQuery, List<GetAllInstructionByRecipeIdQueryResult>>
    {
        private readonly IInstructionRepository _instructionRepository;
        public GetAllInstructionByRecipeIdQueryHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task<List<GetAllInstructionByRecipeIdQueryResult>> Handle(GetAllInstructionByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            var entityFound = await _instructionRepository.GetAllInstructionByRecipeIdAsync(request.Id);
            return entityFound.OrderBy(ob => ob.Step).Select(s =>
                new GetAllInstructionByRecipeIdQueryResult(
                    s.Id,
                    s.Step,
                    s.StepName,
                    s.StepInstruction
                    )
            ).ToList();
        }
    }
}
