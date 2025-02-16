using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById
{
    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, GetRecipeByIdQueryResult>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<GetRecipeByIdQueryHandler> _logger;
        public GetRecipeByIdQueryHandler(IRecipesRepository recipeRepository, ILogger<GetRecipeByIdQueryHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task<GetRecipeByIdQueryResult> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                Domain.Entity.Recipe recipeFound = await _recipeRepository.GetAsync(request.Id);
                if (recipeFound is null)
                {
                    throw new RecipeNotFoundException("invalide key", $"Recipe with Id {request.Id} not found");
                }
                _logger.LogInformation($"GetRecipeByIdQueryHandler : recipe {request.Id} found");
                return new GetRecipeByIdQueryResult(
                    recipeFound.Id,
                    recipeFound.Name,
                    recipeFound.Ingredients,
                    recipeFound.Instructions,
                    recipeFound.RecipyDifficulty,
                    recipeFound.TimeToPrepareRecipe,
                    recipeFound.NbGuest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ;
            }
        }
    }
}
