using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.CheckRecipeIngredientAcces
{
    public class CheckRecipeIngredientAccesQueryHandler : IRequestHandler<CheckRecipeIngredientAccesQuery, bool>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<GetAllInstructionByRecipeIdQueryHandler> _logger;
        public CheckRecipeIngredientAccesQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, IRecipesRepository recipesRepository, ILogger<GetAllInstructionByRecipeIdQueryHandler> logger)
        {
            _recipesRepository = recipesRepository;
            _recipeIngredientRepository = recipeIngredientRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckRecipeIngredientAccesQuery request, CancellationToken cancellationToken)
        {
            if (request.RecipeIngredientId.IsEmpty())
            {
                throw new WrongParameterException("Invalide Key", "InstructionId is empty");
            }
            if (request.UserId.IsEmpty())
            {
                throw new WrongParameterException("Invalide Key", "UserId is empty");
            }
            var recipeIngredient = await _recipeIngredientRepository.GetAsync(request.RecipeIngredientId);
            if (recipeIngredient is null)
            {
                throw new InstructionNotFoundException("NotFound", $"Instruction with {request.RecipeIngredientId} not found");
            }
            var recipe = await _recipesRepository.GetAsync((Guid)recipeIngredient.RecipeId);
            if (recipe is null)
            {
                if (recipe.UserId == request.UserId)
                    return true;
            }
            return false;
        }
    }
}
