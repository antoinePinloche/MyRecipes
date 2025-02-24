using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Query.CheckInstructionAcces
{
    public class CheckInstructionAccesQueryHandler : IRequestHandler<CheckInstructionAccesQuery, bool>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<CheckInstructionAccesQueryHandler> _logger;
        public CheckInstructionAccesQueryHandler(IInstructionRepository instructionRepository, IRecipesRepository recipesRepository, ILogger<CheckInstructionAccesQueryHandler> logger)
        {
            _recipesRepository = recipesRepository;
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckInstructionAccesQuery request, CancellationToken cancellationToken)
        {
            if (request.InstructionId.IsEmpty())
            {
                throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CheckInstructionAccesQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.INGREDIENT_ID);
            }
            if (request.UserId.IsEmpty())
            {
                throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CheckInstructionAccesQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.USER_ID);
            }
            var instruction = await _instructionRepository.GetAsync(request.InstructionId);
            if (instruction is null)
            {
                throw new InstructionNotFoundException(_logger,
                        nameof(Handle),
                        "CheckInstructionAccesQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Instruction with {request.InstructionId} not found");
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
