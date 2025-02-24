using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Constant;
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
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "DeleteFoodTypeByIdCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var entity = await _foodTypeRepository.GetAsync(request.Id);
                if (entity is null)
                    throw new FoodTypeNotFoundException(_logger,
                        nameof(Handle),
                        "DeleteFoodTypeByIdCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_KEY, $"FoodType with id : {request.Id.ToString()} not found");
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
