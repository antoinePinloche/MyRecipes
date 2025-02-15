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
using MyRecipes.Transverse.Exception;

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
            try
            {
                var recipeIngredientFound = await _sender.Send(new GetRecipeIngredientByIdQuery(request.Id));
                if (recipeIngredientFound is null)
                {
                    throw new RecipeIngredientNotFoundException("NotFound", $"RecipeIngredient not found with Id {request.Id}");
                }
                Domain.Entity.RecipeIngredient recipeIngredientToDelete = new()
                {
                    Id = recipeIngredientFound.Id,
                    IngredientId = recipeIngredientFound.IngredientId,
                    Ingredient = recipeIngredientFound.Ingredient,
                    Quantity = recipeIngredientFound.Quantity,
                    Unit = recipeIngredientFound.Unit,
                    RecipeId = recipeIngredientFound.RecipeId
                };
                await _recipeIngredientRepository.RemoveAsync(recipeIngredientToDelete);
            }
            catch (RecipeIngredientNotFoundException ex)
            {
                throw new RecipeIngredientNotFoundException(ex.Error, ex.Message);
            }
        }
    }
}
