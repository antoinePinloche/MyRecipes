﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient
{
    /// <summary>
    /// handler de la command <see cref="CreateIngredientCommand"/>
    /// </summary>
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<CreateIngredientCommandHandler> _logger;
        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository, ILogger<CreateIngredientCommandHandler> logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        public async Task Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Name.IsNullOrEmpty())
                {
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "CreateIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.NAME);
                }
                if (request.FoodTypeId.IsEmpty())
                {
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "CreateIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.FOOD_TYPE_ID);
                }
                var entity = await _ingredientRepository.HasIngredient(request.Name);
                if (entity is not null)
                {
                    throw new IngredientAlreadyExistException(
                        _logger,
                        nameof(Handle),
                        "CreateIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.CONFLICT,
                        $"Ingredient with Name {request.Name} already exist");
                }
                Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient() { Id = Guid.NewGuid(), FoodTypeId = request.FoodTypeId, Name = request.Name };
                await _ingredientRepository.AddAsync(ingredient);
                _logger.LogInformation($"CreateIngredientCommand : Ingredient {request.Name} create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
