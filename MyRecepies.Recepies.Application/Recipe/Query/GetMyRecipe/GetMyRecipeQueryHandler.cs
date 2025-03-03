using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetMyRecipe
{
    /// <summary>
    /// Handler de la query <see cref="GetMyRecipeQuery"/>
    /// </summary>
    public class GetMyRecipeQueryHandler : IRequestHandler<GetMyRecipeQuery, List<GetMyRecipeQueryResult>>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<GetMyRecipeQueryHandler> _logger;
        public GetMyRecipeQueryHandler(IRecipesRepository recipeRepository, ILogger<GetMyRecipeQueryHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task<List<GetMyRecipeQueryResult>> Handle(GetMyRecipeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "GetMyRecipeQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                var recipeFound = await _recipeRepository.GetByRecipeIdAsync(request.Id);
                if (recipeFound.IsNullOrEmpty())
                {
                    throw new RecipeNotFoundException(_logger,
                        nameof(Handle),
                        "GetMyRecipeQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.RECIPE_NOT_FOUND);
                }
                _logger.LogInformation($"GetMyRecipeQueryHandler : recipe {request.Id} found");
                return recipeFound.Select(s =>
                    new GetMyRecipeQueryResult(
                        s.Id,
                        s.Name,
                        s.Ingredients,
                        s.Instructions,
                        s.RecipyDifficulty,
                        s.TimeToPrepareRecipe,
                        s.NbGuest)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ;
            }
        }
    }
}
