using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    public class GetFoodTypeByIdQueryHandler : IRequestHandler<GetFoodTypeByIdQuery, GetFoodTypeByIdQueryResult>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        private readonly ILogger<GetFoodTypeByIdQueryHandler> _logger;
        public GetFoodTypeByIdQueryHandler(IFoodTypeRepository foodTypeRepository, ILogger<GetFoodTypeByIdQueryHandler> logger)
        {
            _foodTypeRepository = foodTypeRepository; 
            _logger = logger;
        }

        public async Task<GetFoodTypeByIdQueryResult> Handle(GetFoodTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "GetFoodTypeByIdQueryHandler", 
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var result = await _foodTypeRepository.GetAsync(request.Id);
                _logger.LogInformation($"GetFoodTypeByIdQueryHandler : found FoodType {request.Id}");
                return new GetFoodTypeByIdQueryResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
