using MediatR;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient
{
    public class DeleteRecipeIngredientCommandHandler : IRequestHandler<DeleteRecipeIngredientCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ISender _sender;
        public DeleteRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
        }
        public async Task Handle(DeleteRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            var recipeIngredientFound = await _sender.Send(new GetRecipeIngredientByIdQuery(request.Id));
            if (recipeIngredientFound is not null)
            {
                Domain.Entity.RecipeIngredient recipeIngredientToDelete = new ()
                {
                    Id = recipeIngredientFound.Id,
                    IngredientId = recipeIngredientFound.IngredientId,
                    Ingredient = recipeIngredientFound.Ingredient,
                    Quantity = recipeIngredientFound.Quantity,
                    Unit = recipeIngredientFound.Unit,
                    //RecipeId = recipeIngredientFound.RecipeId,
                    //Recipe = recipeIngredientFound.Recipe
                };
                await _recipeIngredientRepository.RemoveAsync(recipeIngredientToDelete);
            }
        }
    }
}
