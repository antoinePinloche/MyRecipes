using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Query.CheckRecipeAcces
{
    public class CheckRecipeAccesQueryHandler : IRequestHandler<CheckRecipeAccesQuery, bool>
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<CheckRecipeAccesQueryHandler> _logger;

        public CheckRecipeAccesQueryHandler(IRecipesRepository recipeRepository, ILogger<CheckRecipeAccesQueryHandler> logger)
        {
            _recipesRepository = recipeRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckRecipeAccesQuery request, CancellationToken cancellationToken)
        {
            if (request.RecipeId.IsEmpty())
            {
                throw new WrongParameterException("Invalide Key", "InstructionId is empty");
            }
            if (request.UserId.IsEmpty())
            {
                throw new WrongParameterException("Invalide Key", "UserId is empty");
            }
            var recipe = await _recipesRepository.GetAsync(request.RecipeId);
            if (recipe is null)
            {
                if (recipe.UserId == request.UserId)
                    return true;
            }
            return false;
        }
    }
}
