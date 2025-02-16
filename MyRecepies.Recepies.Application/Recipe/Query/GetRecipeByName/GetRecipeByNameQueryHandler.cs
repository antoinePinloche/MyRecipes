using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
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
                    throw new WrongParameterException("Invalide parameter", "Name is invalide");
                ICollection<Domain.Entity.Recipe> recipes = await _recipeRepository.GetByNameAsync(request.Name);
                if (recipes.IsNullOrEmpty())
                {
                    throw new RecipeNotFoundException("invalide key", $"Recipe(s) not found Name contain {request.Name}");
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
