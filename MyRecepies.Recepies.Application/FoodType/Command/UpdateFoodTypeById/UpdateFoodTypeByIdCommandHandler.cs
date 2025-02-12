using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById
{

    public class UpdateFoodTypeByIdCommandHandler : IRequestHandler<UpdateFoodTypeByIdCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        public UpdateFoodTypeByIdCommandHandler(IFoodTypeRepository foodTypeRepository) => _foodTypeRepository = foodTypeRepository;

        public async Task Handle(UpdateFoodTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var entity = await _foodTypeRepository.GetAsync(request.Id);
            if (entity == null)
            {
                throw new Exception($"FoodType not found {request.Id}");
            }
            if (await _foodTypeRepository.FoodTypeByName(request.Name))
            {
                throw new Exception($"FoodType Already Exist {request.Name}");
            }
            entity.Name = request.Name;
            await _foodTypeRepository.UpdateAsync(entity);
        }
    }
}
