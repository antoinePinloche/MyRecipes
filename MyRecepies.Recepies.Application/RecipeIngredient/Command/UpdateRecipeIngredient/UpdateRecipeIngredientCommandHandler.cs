using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient
{
    public class UpdateRecipeIngredientCommandHandler : IRequestHandler<UpdateRecipeIngredientCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ISender _sender;
        public UpdateRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, IIngredientRepository ingredientRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _ingredientRepository = ingredientRepository;
            _sender = sender;
        }

        /*
        {
  "ingredientId": "27fe793d-407d-4654-9f7a-72248bfcbc05",
  "quantity": 0,
  "unit": 0,
  "recipeId": "00000000-0000-0000-0000-000000000000"
}
        */
        public async Task Handle(UpdateRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
                var riFound = await _recipeIngredientRepository.GetAsync(request.Id);
                if (riFound is not null)
                {
                    if (riFound.IngredientId != request.IngredientId)
                    {
                        riFound.IngredientId = request.IngredientId;
                        var ingredientFound = await _ingredientRepository.GetAsync(request.IngredientId);
                        riFound.Ingredient = ingredientFound;
                    }
                    //riFound.RecipeId = request.RecipeId;
                    riFound.Unit = request.Unit;
                    riFound.Quantity = request.Quantity;
                    await _recipeIngredientRepository.UpdateAsync(riFound);
                }
        }
    }
}
