using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System.Diagnostics.CodeAnalysis;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction
{
    public class CreateInstructionCommandHandler : IRequestHandler<CreateInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<CreateInstructionCommandHandler> _logger;
        public CreateInstructionCommandHandler(IInstructionRepository instructionRepository, ILogger<CreateInstructionCommandHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }
        public async Task Handle(CreateInstructionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (request.StepInstruction.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "StepInstruction is invalide");
                }
                if (request.StepName.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "StepName is invalide");
                }
                if (request.RecipeId.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "RecipeId is invalide");
                }
                var instructionRecipe = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)request.RecipeId);
                if (instructionRecipe is not null)
                {
                    if (instructionRecipe.Any(a => a.Step == request.Step))
                    {
                        throw new InstructionAlreadyExisteException("Can't Create instruction step already Exist", $"Instruction {request.Step} already Exist");
                    }
                }

                Domain.Entity.Instruction instructionToAdd = new()
                {
                    Id = Guid.NewGuid(),
                    RecipeId = request.RecipeId,
                    Step = request.Step,
                    StepName = request.StepName,
                    StepInstruction = request.StepInstruction
                };
                await _instructionRepository.AddAsync(instructionToAdd);
                _logger.LogInformation("CreateInstructionCommandHandler : Instruction create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
