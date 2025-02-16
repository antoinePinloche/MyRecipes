using MediatR;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
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
            if (request.Name.IsNullOrEmpty())
                throw new WrongParameterException("Invalide parameter", "Name is invalide");
            try
            {
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
                return listToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
