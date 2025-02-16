using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    public class CreateFoodTypeCommandHandler : IRequestHandler<CreateFoodTypeCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        public CreateFoodTypeCommandHandler(IFoodTypeRepository foodTypeRepository) => _foodTypeRepository = foodTypeRepository;

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
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
