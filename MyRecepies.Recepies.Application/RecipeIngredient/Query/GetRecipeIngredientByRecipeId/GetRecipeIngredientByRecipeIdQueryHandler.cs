using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId
{
    public class GetRecipeIngredientByRecipeIdQueryHandler : IRequestHandler<GetRecipeIngredientByRecipeIdQuery, List<GetRecipeIngredientByRecipeIdQueryResult>>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<GetRecipeIngredientByRecipeIdQueryHandler> _logger;
        public GetRecipeIngredientByRecipeIdQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, IRecipesRepository recipesRepository, ILogger<GetRecipeIngredientByRecipeIdQueryHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _recipesRepository = recipesRepository;
            _logger = logger;
        }

        public async Task<List<GetRecipeIngredientByRecipeIdQueryResult>> Handle(GetRecipeIngredientByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "GetRecipeIngredientByRecipeIdQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.RECIPE_INGREDIENT_ID);
                }
                var recipeFound = await _recipesRepository.GetAsync(request.Id);
                if (recipeFound is null)
                {
                    throw new RecipeNotFoundException(_logger,
                        nameof(Handle),
                        "GetRecipeIngredientByRecipeIdQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Recipe notfound with ID {request.Id}");
                }
                var entityFound = await _recipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync(request.Id);
                if (entityFound.IsNullOrEmpty())
                {
                    throw new RecipeIngredientNotFoundException(_logger,
                        nameof(Handle),
                        "GetRecipeIngredientByRecipeIdQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"RecipeIngredient Not found for recipe {request.Id}");
                }
                _logger.LogInformation($"GetRecipeIngredientByRecipeIdQueryHandler : return all recipe ingredient for recipe {request.Id}");
                return entityFound.Select(s => 
                new GetRecipeIngredientByRecipeIdQueryResult(
                    s.Id,
                    s.IngredientId,
                    s.Ingredient,
                    s.Quantity,
                    s.Unit)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
