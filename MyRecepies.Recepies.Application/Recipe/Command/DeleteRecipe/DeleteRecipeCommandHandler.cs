﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId;
using MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe
{
    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ISender _sender;
        private readonly ILogger<DeleteRecipeCommandHandler> _logger;
        public DeleteRecipeCommandHandler(IRecipesRepository recipeRepository, ISender sender, ILogger<DeleteRecipeCommandHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _sender = sender;
            _logger = logger;
        }

        public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                Domain.Entity.Recipe recipeFound = await _recipeRepository.GetAsync(request.Id);
                if (recipeFound is null)
                {
                    throw new RecipeNotFoundException("invalide key", $"Recipe with Id {request.Id} not found");
                }
                if (recipeFound.Ingredients is null || recipeFound.Ingredients.Any())
                {
                    await _sender.Send(new DeleteRecipeIngredientByRecipeIdCommand(request.Id));
                    recipeFound.Ingredients = null;
                }
                if (recipeFound.Instructions is null || recipeFound.Instructions.Any())
                {
                    await _sender.Send(new DeleteInstructionByRecipeIdCommand(request.Id));
                    recipeFound.Instructions = null;
                }
                await _recipeRepository.RemoveAsync(recipeFound);
                _logger.LogInformation($"DeleteRecipeCommandHandler : recipe {request.Id} delete");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
