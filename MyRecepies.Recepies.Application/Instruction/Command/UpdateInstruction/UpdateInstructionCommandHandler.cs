using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
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
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                if (request.StepInstruction.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "StepInstruction is invalide");
                }
                if (request.StepName.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "StepName is invalide");
                }
                var entityFound = await _instructionRepository.GetAsync(request.Id);
                if (entityFound is null)
                {
                    throw new InstructionNotFoundException("Invalide Key", $"Instruction {request.Id} not found");
                }
                var allInstruction = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)entityFound.RecipeId);
                if (entityFound.StepInstruction != request.StepInstruction)
                    entityFound.StepInstruction = request.StepInstruction;
                if (entityFound.StepName != request.StepName)
                    entityFound.StepName = request.StepName;
                if (allInstruction.Any(w => w.Step == request.Step && w.Id != entityFound.Id))
                    throw new InstructionAlreadyExisteException("Can't update step already Exist", $"Instruction {request.Id} already Exist");
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
