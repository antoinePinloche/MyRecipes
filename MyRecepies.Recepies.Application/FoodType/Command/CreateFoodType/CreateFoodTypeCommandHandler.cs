using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    /// <summary>
    /// handler de la command <see cref="CreateFoodTypeCommand"/>
    /// </summary>
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
                throw new WrongParameterException(
                    _logger,
                    nameof(Handle),
                    "CreateFoodTypeCommandHandler",
                    Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                    Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.NAME);
            }
            if (!(await _foodTypeRepository.FoodTypeExist(request.Name)))
            {
                throw new FoodTypeAlreadyExistException(
                    _logger,
                    nameof(Handle),
                    "CreateFoodTypeCommandHandler",
                    Constant.EXCEPTION.TITLE.CONFLICT,
                    $"FoodType {request.Name} alreadyExist");
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
