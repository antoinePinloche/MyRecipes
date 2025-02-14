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
                throw new FoodTypeNotFoundException("Not Found", $"FoodType with {request.Id} can't be found");
            }
            if (await _foodTypeRepository.FoodTypeByName(request.Name))
            {
                throw new FoodTypeAlreadyExistException("invalide creation", $"FoodType {entity.Name} already exist");
            }
            entity.Name = request.Name;
            await _foodTypeRepository.UpdateAsync(entity);
        }
    }
}
