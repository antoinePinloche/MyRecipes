﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient
{
    /// <summary>
    /// handler de la command <see cref="DeleteIngredientCommand"/>
    /// </summary>
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<DeleteIngredientCommandHandler> _logger;
        public DeleteIngredientCommandHandler(IIngredientRepository ingredientRepository, ILogger<DeleteIngredientCommandHandler> logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "DeleteIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var ingredient = await _ingredientRepository.GetAsync(request.Id);
                if (ingredient is null)
                {
                    throw new IngredientNotFoundException(
                        _logger,
                        nameof(Handle),
                        "DeleteIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Ingredient not found {request.Id}");
                }
                await _ingredientRepository.RemoveAsync(ingredient);
                _logger.LogInformation($"DeleteIngredientCommand : Ingredient {ingredient.Name} delete");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
