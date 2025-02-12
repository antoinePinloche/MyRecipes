using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById
{
    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, GetRecipeByIdQueryResult>
    {
        private readonly IRecipesRepository _recipeRepository;

        public GetRecipeByIdQueryHandler(IRecipesRepository recipeRepository) => _recipeRepository = recipeRepository;

        public async Task<GetRecipeByIdQueryResult> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new Exception();
            if (request.Id.IsEmpty())
                throw new Exception();
            try
            {
                Domain.Entity.Recipe recipe = await _recipeRepository.GetAsync(request.Id);
                return new GetRecipeByIdQueryResult(recipe.Id, recipe.Name, recipe.Ingredients, recipe.Instructions, recipe.RecipyDifficulty, recipe.TimeToPrepareRecipe, recipe.NbGuest);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
