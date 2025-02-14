using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById
{
    internal class DeleteFoodTypeByIdCommandHandler : IRequestHandler<DeleteFoodTypeByIdCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        public DeleteFoodTypeByIdCommandHandler(IFoodTypeRepository foodTypeRepository) => _foodTypeRepository = foodTypeRepository;

        public async Task Handle(DeleteFoodTypeByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _foodTypeRepository.GetAsync(request.Id);
                if (entity is null)
                    throw new FoodTypeNotFoundException("Invalide key", $"FoodType with id : {request.Id.ToString()} not found");
                await _foodTypeRepository.RemoveAsync(entity);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
