using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById
{

    public class UpdateFoodTypeByIdCommandHandler : IRequestHandler<UpdateFoodTypeByIdCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ILogger<UpdateFoodTypeByIdCommandHandler> _logger;
        public UpdateFoodTypeByIdCommandHandler(IFoodTypeRepository foodTypeRepository, ILogger<UpdateFoodTypeByIdCommandHandler> logger)
        {
            _foodTypeRepository = foodTypeRepository;
            _logger = logger;
        }
        public async Task Handle(UpdateFoodTypeByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            if (request.Name.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Name is invalide");
            }
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
            _logger.LogInformation($"UpdateFoodTypeByIdCommandHandler : FoodType {request.Id} update");
        }
    }
}
