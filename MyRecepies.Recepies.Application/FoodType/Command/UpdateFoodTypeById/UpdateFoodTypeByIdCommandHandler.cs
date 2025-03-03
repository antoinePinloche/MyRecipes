using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById
{
    /// <summary>
    /// Handler de la commande <see cref="UpdateFoodTypeByIdCommand"/>
    /// </summary>
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
                throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateFoodTypeByIdCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
            }
            if (request.Name.IsNullOrEmpty())
            {
                throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateFoodTypeByIdCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.NAME);
            }
            var entity = await _foodTypeRepository.GetAsync(request.Id);
            if (entity == null)
            {
                throw new FoodTypeNotFoundException(_logger,
                        nameof(Handle),
                        "UpdateFoodTypeByIdCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"FoodType with {request.Id} can't be found");
            }
            if (await _foodTypeRepository.FoodTypeByName(request.Name))
            {
                throw new FoodTypeAlreadyExistException(_logger,
                        nameof(Handle),
                        "UpdateFoodTypeByIdCommandHandler",
                        Constant.EXCEPTION.TITLE.CONFLICT, $"FoodType {entity.Name} already exist");
            }
            entity.Name = request.Name;
            await _foodTypeRepository.UpdateAsync(entity);
            _logger.LogInformation($"UpdateFoodTypeByIdCommandHandler : FoodType {request.Id} update");
        }
    }
}
