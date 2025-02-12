using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient
{
    public class GetAllRecipeIngredientQueryHandler : IRequestHandler<GetAllRecipeIngredientQuery, List<GetAllRecipeIngredientQueryResult>>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ISender _sender;
        public GetAllRecipeIngredientQueryHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
        }

        public async Task<List<GetAllRecipeIngredientQueryResult>> Handle(GetAllRecipeIngredientQuery request, CancellationToken cancellationToken)
        {
            var recipeIngredientList = await _recipeIngredientRepository.GetAllAsync();
            if (recipeIngredientList is not null)
            {
                List<GetAllRecipeIngredientQueryResult> listReturn = recipeIngredientList.Select(s => new GetAllRecipeIngredientQueryResult(s.Id, s.IngredientId, s.Ingredient,
                    s.Quantity, s.Unit, s.RecipeId/*, s.Recipe*/)).ToList();
                return listReturn;
            }
            throw new NotImplementedException();
        }
    }
}
