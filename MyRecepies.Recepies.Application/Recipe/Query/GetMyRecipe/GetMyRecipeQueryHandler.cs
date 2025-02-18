using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetMyRecipe
{
    public class GetMyRecipeQueryHandler : IRequestHandler<GetMyRecipeQuery, List<GetMyRecipeQueryResult>>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<GetRecipeByIdQueryHandler> _logger;
        public GetMyRecipeQueryHandler(IRecipesRepository recipeRepository, ILogger<GetRecipeByIdQueryHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task<List<GetMyRecipeQueryResult>> Handle(GetMyRecipeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                var recipeFound = await _recipeRepository.GetByRecipeIdAsync(request.Id);
                if (recipeFound.IsNullOrEmpty())
                {
                    throw new RecipeNotFoundException("invalide key", $"Recipe not found");
                }
                _logger.LogInformation($"GetRecipeByIdQueryHandler : recipe {request.Id} found");
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
