using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe
{
    public class GetAllRecipeQueryHandler : IRequestHandler<GetAllRecipeQuery, List<GetAllRecipeQueryResult>>
    {
        private readonly IRecipesRepository _recipeRepository;

        public GetAllRecipeQueryHandler(IRecipesRepository recipeRepository) => _recipeRepository = recipeRepository;

        public async Task<List<GetAllRecipeQueryResult>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
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
            return listToReturn;
        }
    }
}
