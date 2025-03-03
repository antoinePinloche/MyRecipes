using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById
{
    /// <summary>
    /// Handler de la query <see cref="GetInstructionByIdQuery"/>
    /// </summary>
    public class GetInstructionByIdQueryHandler : IRequestHandler<GetInstructionByIdQuery, GetInstructionByIdQueryResult>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<GetInstructionByIdQueryHandler> _logger;
        public GetInstructionByIdQueryHandler(IInstructionRepository instructionRepository, ILogger<GetInstructionByIdQueryHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task<GetInstructionByIdQueryResult> Handle(GetInstructionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "GetInstructionByIdQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var entityFound = await _instructionRepository.GetAsync(request.Id);
                if (entityFound is null)
                    throw new InstructionNotFoundException(_logger,
                        nameof(Handle),
                        "GetInstructionByIdQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Instruction for recipe {request.Id} not found");
                _logger.LogInformation($"GetInstructionByIdQueryHandler : instruction {request.Id} found");
                return new GetInstructionByIdQueryResult(entityFound.Id, entityFound.Step, entityFound.StepName, entityFound.StepInstruction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
