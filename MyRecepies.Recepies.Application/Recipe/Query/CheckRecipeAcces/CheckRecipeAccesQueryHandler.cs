using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Query.CheckRecipeAcces
{
    /// <summary>
    /// Handler de la query <see cref="CheckRecipeAccesQuery"/>
    /// </summary>
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
                throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CheckRecipeAccesQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.RECIPE_ID);
            }
            if (request.UserId.IsEmpty())
            {
                throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CheckRecipeAccesQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.USER_ID);
            }
            var recipe = await _recipesRepository.GetAsync(request.RecipeId);
            if (recipe is null)
            {
                if (recipe?.UserId == request.UserId)
                    return true;
            }
            return false;
        }
    }
}
