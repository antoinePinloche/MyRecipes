using MediatR;
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
        private readonly ISender _sender;
        public GetRecipeIngredientByRecipeIdQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
        }

        public async Task<List<GetRecipeIngredientByRecipeIdQueryResult>> Handle(GetRecipeIngredientByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new Exception();
            }
            if (request.Id.IsEmpty())
            {
                throw new Exception();
            }
            try
            {
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
            catch(RecipeIngredientNotFoundException ex)
            {
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
