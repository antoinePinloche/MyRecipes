using MediatR;
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
                {
                    throw new Exception($"Not found FoodEntity with id {request.Id}");
                }
                await _foodTypeRepository.RemoveAsync(entity);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
