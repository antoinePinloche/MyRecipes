using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe
{
    /// <summary>
    /// Handler de la query <see cref="GetAllRecipeQuery"/>
    /// </summary>
    public class GetAllRecipeQueryHandler : IRequestHandler<GetAllRecipeQuery, List<GetAllRecipeQueryResult>>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<GetAllRecipeQueryHandler> _logger;
        public GetAllRecipeQueryHandler(IRecipesRepository recipeRepository, ILogger<GetAllRecipeQueryHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task<List<GetAllRecipeQueryResult>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ICollection<Domain.Entity.Recipe> recipeList = await _recipeRepository.GetAllAsync();
                var listToReturn = recipeList.Select(s => new GetAllRecipeQueryResult(
                    s.Id,
                    s.Name,
                    s.Ingredients,
                    s.Instructions,
                    s.RecipyDifficulty,
                    s.TimeToPrepareRecipe,
                    s.NbGuest
                    )).ToList();
                _logger.LogInformation("GetAllRecipeQueryHandler : recipe return");
                return listToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
