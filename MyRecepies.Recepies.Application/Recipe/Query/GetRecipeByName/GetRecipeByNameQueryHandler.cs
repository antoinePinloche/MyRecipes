using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
    /// <summary>
    /// Handler de la query <see cref="GetRecipeByNameQuery"/>
    /// </summary>
    public class GetRecipeByNameQueryHandler : IRequestHandler<GetRecipeByNameQuery, List<GetRecipeByNameQueryResult>>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<GetRecipeByNameQueryHandler> _logger;
        public GetRecipeByNameQueryHandler(IRecipesRepository recipeRepository, ILogger<GetRecipeByNameQueryHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task<List<GetRecipeByNameQueryResult>> Handle(GetRecipeByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Name.IsNullOrEmpty())
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "GetRecipeByNameQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.NAME);
                ICollection<Domain.Entity.Recipe> recipes = await _recipeRepository.GetByNameAsync(request.Name);
                if (recipes.IsNullOrEmpty())
                {
                    throw new RecipeNotFoundException(_logger,
                        nameof(Handle),
                        "GetRecipeByNameQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Recipe(s) not found Name contain {request.Name}");
                }
                var listToReturn = recipes.Select(s => new GetRecipeByNameQueryResult(
                        s.Id,
                        s.Name,
                        s.Ingredients,
                        s.Instructions,
                        s.RecipyDifficulty,
                        s.TimeToPrepareRecipe,
                        s.NbGuest
                        )).ToList();
                _logger.LogInformation($"GetRecipeByNameQueryHandler : recipe(s) for research {request.Name} found");
                return listToReturn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
