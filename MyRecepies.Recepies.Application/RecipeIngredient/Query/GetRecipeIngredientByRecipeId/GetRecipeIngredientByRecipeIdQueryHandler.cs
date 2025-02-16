using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId
{
    public class GetRecipeIngredientByRecipeIdQueryHandler : IRequestHandler<GetRecipeIngredientByRecipeIdQuery, List<GetRecipeIngredientByRecipeIdQueryResult>>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ISender _sender;
        public GetRecipeIngredientByRecipeIdQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, IRecipesRepository recipesRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _recipesRepository = recipesRepository;
            _sender = sender;
        }

        public async Task<List<GetRecipeIngredientByRecipeIdQueryResult>> Handle(GetRecipeIngredientByRecipeIdQuery request, CancellationToken cancellationToken)
        {

            if (request.Id.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            try
            {
                var recipeFound = await _recipesRepository.GetAsync(request.Id);
                if (recipeFound is null)
                {
                    throw new RecipeNotFoundException("Invalide Key", $"Recipe notfound with ID {request.Id}");
                }
                var entityFound = await _recipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync(request.Id);
                if (entityFound.IsNullOrEmpty())
                {
                    throw new RecipeIngredientNotFoundException("NotFound", $"RecipeIngredient Not found for recipe {request.Id}");
                }
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
                throw;
            }
        }
    }
}
