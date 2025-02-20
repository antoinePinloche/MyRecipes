﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById
{
    public class DeleteFoodTypeByIdCommandHandler : IRequestHandler<DeleteFoodTypeByIdCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ILogger<DeleteFoodTypeByIdCommandHandler> _logger;
        public DeleteFoodTypeByIdCommandHandler(IFoodTypeRepository foodTypeRepository, ILogger<DeleteFoodTypeByIdCommandHandler> logger)
        {
            _foodTypeRepository = foodTypeRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteFoodTypeByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                var entity = await _foodTypeRepository.GetAsync(request.Id);
                if (entity is null)
                    throw new FoodTypeNotFoundException("Invalide key", $"FoodType with id : {request.Id.ToString()} not found");
                await _foodTypeRepository.RemoveAsync(entity);
                _logger.LogInformation($"DeleteFoodTypeByIdCommandHandler : FoodType {entity.Name} Delete");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
