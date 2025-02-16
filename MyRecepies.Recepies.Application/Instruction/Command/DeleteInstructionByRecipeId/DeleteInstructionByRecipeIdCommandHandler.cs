using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
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
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                var RecipeIngredientList = await _instructionRepository.GetAllInstructionByRecipeIdAsync(request.Id);

                if (RecipeIngredientList.IsNullOrEmpty())
                {
                    throw new InstructionNotFoundException("Invalide Key", $"Instructions for Recipe {request.Id} not found");
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
