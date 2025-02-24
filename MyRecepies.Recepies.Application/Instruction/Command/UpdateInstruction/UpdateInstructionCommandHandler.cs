using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Command.UpdateInstruction
{
    public class UpdateInstructionCommandHandler : IRequestHandler<UpdateInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<UpdateInstructionCommandHandler> _logger;
        public UpdateInstructionCommandHandler(IInstructionRepository instructionRepository, ILogger<UpdateInstructionCommandHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateInstructionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                if (request.StepInstruction.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.STEP_INSTRUCTION);
                }
                if (request.StepName.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.STEP_NAME);
                }
                var entityFound = await _instructionRepository.GetAsync(request.Id);
                if (entityFound is null)
                {
                    throw new InstructionNotFoundException(_logger,
                        nameof(Handle),
                        "UpdateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_KEY,
                        $"Instruction {request.Id} not found");
                }
                var allInstruction = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)entityFound.RecipeId);
                if (entityFound.StepInstruction != request.StepInstruction)
                    entityFound.StepInstruction = request.StepInstruction;
                if (entityFound.StepName != request.StepName)
                    entityFound.StepName = request.StepName;
                if (allInstruction.Any(w => w.Step == request.Step && w.Id != entityFound.Id))
                    throw new InstructionAlreadyExisteException(_logger,
                        nameof(Handle),
                        "UpdateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INSTRUCTION_DUPLICATION_UPDATE,
                        $"Instruction {request.Id} already Exist");
                entityFound.Step = request.Step;
                await _instructionRepository.UpdateAsync(entityFound);
                _logger.LogInformation($"UpdateInstructionCommandHandler : Instruction {request.Id} update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
