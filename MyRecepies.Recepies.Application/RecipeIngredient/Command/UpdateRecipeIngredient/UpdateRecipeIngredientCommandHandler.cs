using Azure.Core;
using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
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
        private readonly IRecipesRepository _recipesRepository;
        private readonly ISender _sender;
        public UpdateRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, IIngredientRepository ingredientRepository, IRecipesRepository recipesRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _ingredientRepository = ingredientRepository;
            _recipesRepository = recipesRepository;
            _sender = sender;
        }
        public async Task Handle(UpdateRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.RecipeId.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide key", $"request paramater RecipeID is null or empty");
                }
                if (request.IngredientId.IsEmpty())
                {
                    throw new WrongParameterException("Invalide key", $"request paramater IngredientId is empty");
                }
                var riFound = await _recipeIngredientRepository.GetAsync(request.Id);
                if (riFound is null)
                {
                    throw new RecipeIngredientNotFoundException("Invalide Key", $"Ingredient Recipe with Id {request.Id} not found");
                }
                if (!request.RecipeId.IsNullOrEmpty())
                {
                    var recipe = await _recipesRepository.GetAsync((Guid)request.RecipeId);
                    if (recipe is null)
                    {
                        throw new RecipeNotFoundException("Invalide key", $"Recipe with ID {request.RecipeId} not found");
                    }
                    var recipeIngredients = await _recipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync((Guid)request.RecipeId);
                    if (recipeIngredients.Any(ri => ri.IngredientId == request.IngredientId))
                    {
                        throw new RecipeIngredientAlreadyExistException("invalide key", $"Recipe with ingredient {request.IngredientId} already exist");
                    }
                }

                if (riFound.IngredientId != request.IngredientId)
                {
                    riFound.IngredientId = request.IngredientId;
                    var ingredientFound = await _ingredientRepository.GetAsync(request.IngredientId);
                    riFound.Ingredient = ingredientFound;
                }

                if (riFound.RecipeId != request.RecipeId)
                    riFound.RecipeId = request.RecipeId;
                riFound.Unit = request.Unit;
                riFound.Quantity = request.Quantity;
                await _recipeIngredientRepository.UpdateAsync(riFound);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
