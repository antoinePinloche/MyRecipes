using MediatR;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
    public class GetRecipeByNameQueryHandler : IRequestHandler<GetRecipeByNameQuery, List<GetRecipeByNameQueryResult>>
    {
        private readonly IRecipesRepository _recipeRepository;

        public GetRecipeByNameQueryHandler(IRecipesRepository recipeRepository) => _recipeRepository = recipeRepository;

        public async Task<List<GetRecipeByNameQueryResult>> Handle(GetRecipeByNameQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new Exception();
            if (request.Name.IsNullOrEmpty())
                throw new Exception();
            try
            {
                ICollection<Domain.Entity.Recipe> recipes = await _recipeRepository.GetByNameAsync(request.Name);
                var listToReturn = recipes.Select(s => new GetRecipeByNameQueryResult(
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
            catch (Exception ex)
            {
                throw new Exception();
            }
            
            throw new NotImplementedException();
        }
    }
}
