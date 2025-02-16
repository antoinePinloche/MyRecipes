using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient
{
    public class GetAllRecipeIngredientQueryHandler : IRequestHandler<GetAllRecipeIngredientQuery, List<GetAllRecipeIngredientQueryResult>>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ILogger<GetAllRecipeIngredientQueryHandler> _logger;
        public GetAllRecipeIngredientQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, ILogger<GetAllRecipeIngredientQueryHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _logger = logger;
        }

        public async Task<List<GetAllRecipeIngredientQueryResult>> Handle(GetAllRecipeIngredientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var recipeIngredientList = await _recipeIngredientRepository.GetAllAsync();
                if (recipeIngredientList is not null)
                {
                    List<GetAllRecipeIngredientQueryResult> listReturn = recipeIngredientList.Select(s => new GetAllRecipeIngredientQueryResult(s.Id, s.IngredientId, s.Ingredient,
                        s.Quantity, s.Unit, s.RecipeId)).ToList();
                    _logger.LogInformation("GetAllRecipeIngredientQueryHandler : return recipe ingredient");
                    return listReturn;
                }
                _logger.LogInformation("GetAllRecipeIngredientQueryHandler : finish wihtout error with empty list");
                return new List<GetAllRecipeIngredientQueryResult>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
