using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    public class CreateFoodTypeCommandHandler : IRequestHandler<CreateFoodTypeCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ILogger<CreateFoodTypeCommandHandler> _logger;
        public CreateFoodTypeCommandHandler(IFoodTypeRepository foodTypeRepository, ILogger<CreateFoodTypeCommandHandler> logger)
        {
            _foodTypeRepository = foodTypeRepository;
            _logger = logger;
        }

        public async Task Handle(CreateFoodTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.Name.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Name is invalide");
            }
            Domain.Entity.FoodType entityToAdd = new Domain.Entity.FoodType()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            try
            {
                var entity = await _foodTypeRepository.AddAsync(entityToAdd);
                _logger.LogInformation($"CreateFoodTypeCommandHandler : FoodType {entity.Id} Create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }
    }
}
