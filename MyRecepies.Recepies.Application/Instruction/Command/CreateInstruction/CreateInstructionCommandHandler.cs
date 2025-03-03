using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction
{
    /// <summary>
    /// Handler de la command <see cref="GetFoodTypeByIdQuery"/>
    /// </summary>
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
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CreateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.STEP_INSTRUCTION);
                }
                if (request.StepName.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CreateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.STEP_NAME);
                }
                if (request.RecipeId.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CreateInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.RECIPE_ID);
                }
                var instructionRecipe = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)request.RecipeId);
                if (instructionRecipe is not null)
                {
                    if (instructionRecipe.Any(a => a.Step == request.Step))
                    {
                        throw new InstructionAlreadyExisteException(
                            _logger,
                            nameof(Handle),
                            "CreateInstructionCommandHandler",
                            Constant.EXCEPTION.TITLE.INSTRUCTION_DUPLICATION_CREATE,
                            Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.DUPLICATION_INSTRUCTION);
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
