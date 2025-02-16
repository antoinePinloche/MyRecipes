using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById
{
    public class GetRecipeIngredientByIdQueryHandler : IRequestHandler<GetRecipeIngredientByIdQuery, GetRecipeIngredientByIdQueryResult>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ISender _sender;
        public GetRecipeIngredientByIdQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
        }
        public async Task<GetRecipeIngredientByIdQueryResult> Handle(GetRecipeIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            try
            {
                Domain.Entity.RecipeIngredient result = await _recipeIngredientRepository.GetAsync(request.Id);
                if (result is null)
                    throw new RecipeIngredientNotFoundException("NotFound", $"RecipeIngredient not found with Id {request.Id}");
                return new GetRecipeIngredientByIdQueryResult(result.Id, result.IngredientId, result.Ingredient,
                    result.Quantity, result.Unit, result.RecipeId);
            }
            catch (Exception ex)
            {
                throw ;
            }

        }
    }
}
