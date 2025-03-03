using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
    /// <summary>
    /// Handler de la query <see cref="GetAllInstructionQuery"/>
    /// </summary>
    public class GetAllInstructionQueryHandler : IRequestHandler<GetAllInstructionQuery, List<GetAllInstructionQueryResult>>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<GetAllInstructionQueryHandler> _logger;
        public GetAllInstructionQueryHandler(IInstructionRepository instructionRepository, ILogger<GetAllInstructionQueryHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task<List<GetAllInstructionQueryResult>> Handle(GetAllInstructionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _instructionRepository.GetAllAsync();
                _logger.LogInformation("GetAllInstructionQueryHandler : finish without Error");
                return res.Select(s =>
                    new GetAllInstructionQueryResult(
                        s.Id,
                        s.Step,
                        s.StepName,
                        s.StepInstruction
                        )
                ).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
