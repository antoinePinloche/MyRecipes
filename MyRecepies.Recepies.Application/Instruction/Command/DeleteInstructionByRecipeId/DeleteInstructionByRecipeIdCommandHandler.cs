using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId
{
    public class DeleteInstructionByRecipeIdCommandHandler : IRequestHandler<DeleteInstructionByRecipeIdCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<DeleteInstructionByRecipeIdCommandHandler> _logger;
        public DeleteInstructionByRecipeIdCommandHandler(IInstructionRepository instructionRepository, ILogger<DeleteInstructionByRecipeIdCommandHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteInstructionByRecipeIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "DeleteInstructionByRecipeIdCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var RecipeIngredientList = await _instructionRepository.GetAllInstructionByRecipeIdAsync(request.Id);

                if (RecipeIngredientList.IsNullOrEmpty())
                {
                    throw new InstructionNotFoundException(_logger,
                        nameof(Handle),
                        "DeleteInstructionByRecipeIdCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_KEY,
                        $"Instructions for Recipe {request.Id} not found");
                }
                await _instructionRepository.RemoveRangeAsync(RecipeIngredientList);
                _logger.LogInformation($"DeleteInstructionByRecipeIdCommandHandler : All instruction(s) delete for recipe {request.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
