using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType
{
    /// <summary>
    /// Handler de la query <see cref="GetAllFoodTypeQuery"/>
    /// </summary>
    public class GetAllFoodTypeQueryHandler : IRequestHandler<GetAllFoodTypeQuery, GetAllFoodTypeQueryResult>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ILogger<GetAllFoodTypeQueryHandler> _logger;
        public GetAllFoodTypeQueryHandler(IFoodTypeRepository foodTypeRepository, ILogger<GetAllFoodTypeQueryHandler> logger) 
        {
            _foodTypeRepository = foodTypeRepository;
            _logger = logger;
        }
        public async Task<GetAllFoodTypeQueryResult> Handle(GetAllFoodTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _foodTypeRepository.GetAllAsync();
            if (result is not null || result.Count() > 0)
            {
                _logger.LogInformation("GetAllFoodTypeQueryHandler : All foodType found");
                return new GetAllFoodTypeQueryResult(result.ToList());
            }
            _logger.LogInformation("GetAllFoodTypeQueryHandler : return nothing");
            return new GetAllFoodTypeQueryResult(new List<Domain.Entity.FoodType>());
        }
    }
}
