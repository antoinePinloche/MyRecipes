using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    public class CreateFoodTypeCommandHandler : IRequestHandler<CreateFoodTypeCommand>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        public CreateFoodTypeCommandHandler(IFoodTypeRepository foodTypeRepository) => _foodTypeRepository = foodTypeRepository;

        public async Task Handle(CreateFoodTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entity.FoodType entityToAdd = new Domain.Entity.FoodType()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            try
            {
                var entity = await _foodTypeRepository.AddAsync(entityToAdd);
                if (entity is null)
                    throw new Exception("FoodType Already Existe");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
