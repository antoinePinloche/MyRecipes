﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Command.UpdateRecipe
{
    /// <summary>
    /// Handler de la command <see cref="UpdateRecipeCommand"/>
    /// </summary>
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<UpdateRecipeCommandHandler> _logger;
        public UpdateRecipeCommandHandler(IRecipesRepository recipeRepository, ILogger<UpdateRecipeCommandHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Name.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateRecipeCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.NAME);
                }
                var entity = await _recipeRepository.GetAsync(request.Id);
                if (entity is null)
                {
                    throw new RecipeNotFoundException(_logger,
                        nameof(Handle),
                        "UpdateRecipeCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Recipe with Id {request.Id} not found");
                }
                if (entity.Name != request.Name)
                    entity.Name = request.Name;
                entity.RecipyDifficulty = request.RecipyDifficulty;
                entity.NbGuest = request.NbGuest;
                entity.TimeToPrepareRecipe = request.TimeToPrepareRecipe;
                await _recipeRepository.UpdateAsync(entity);
                _logger.LogInformation($"UpdateRecipeCommandHandler : recipe {request.Id} update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
