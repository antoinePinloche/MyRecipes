using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
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
            if (request.Id.IsEmpty())
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            try
            {
                Domain.Entity.Recipe recipeFound = await _recipeRepository.GetAsync(request.Id);
                if (recipeFound is null)
                {
                    throw new RecipeNotFoundException("invalide key", $"Recipe with Id {request.Id} not found");
                }
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
                throw ;
            }
        }
    }
}
