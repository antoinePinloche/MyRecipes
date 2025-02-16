using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById
{
    public class GetRecipeIngredientByIdQueryHandler : IRequestHandler<GetRecipeIngredientByIdQuery, GetRecipeIngredientByIdQueryResult>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ILogger<GetRecipeIngredientByIdQueryHandler> _logger;
        public GetRecipeIngredientByIdQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, ILogger<GetRecipeIngredientByIdQueryHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _logger = logger;
        }
        public async Task<GetRecipeIngredientByIdQueryResult> Handle(GetRecipeIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == Guid.Empty)
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                Domain.Entity.RecipeIngredient result = await _recipeIngredientRepository.GetAsync(request.Id);
                if (result is null)
                    throw new RecipeIngredientNotFoundException("NotFound", $"RecipeIngredient not found with Id {request.Id}");
                _logger.LogInformation($"GetRecipeIngredientByIdQueryHandler : recipe ingredient return");
                return new GetRecipeIngredientByIdQueryResult(result.Id, result.IngredientId, result.Ingredient,
                    result.Quantity, result.Unit, result.RecipeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ;
            }

        }
    }
}
