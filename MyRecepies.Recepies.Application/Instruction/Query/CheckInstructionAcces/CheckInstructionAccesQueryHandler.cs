using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Query.CheckInstructionAcces
{
    public class CheckInstructionAccesQueryHandler : IRequestHandler<CheckInstructionAccesQuery, bool>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<GetAllInstructionByRecipeIdQueryHandler> _logger;
        public CheckInstructionAccesQueryHandler(IInstructionRepository instructionRepository, IRecipesRepository recipesRepository, ILogger<GetAllInstructionByRecipeIdQueryHandler> logger)
        {
            _recipesRepository = recipesRepository;
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckInstructionAccesQuery request, CancellationToken cancellationToken)
        {
            if (request.InstructionId.IsEmpty())
            {
                throw new WrongParameterException("Invalide Key", "InstructionId is empty");
            }
            if (request.UserId.IsEmpty())
            {
                throw new WrongParameterException("Invalide Key", "UserId is empty");
            }
            var instruction = await _instructionRepository.GetAsync(request.InstructionId);
            if (instruction is null)
            {
                throw new InstructionNotFoundException("NotFound", $"Instruction with {request.InstructionId} not found");
            }
            var recipe = await _recipesRepository.GetAsync((Guid)instruction.RecipeId);
            if (recipe is null)
            {
                if (recipe.UserId == request.UserId)
                    return true;
            }
            return false;
        }
    }
}
