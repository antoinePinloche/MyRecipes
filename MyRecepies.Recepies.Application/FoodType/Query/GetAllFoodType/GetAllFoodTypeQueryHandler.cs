using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType
{
    public class GetAllFoodTypeQueryHandler : IRequestHandler<GetAllFoodTypeQuery, GetAllFoodTypeQueryResult>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        public GetAllFoodTypeQueryHandler(IFoodTypeRepository foodTypeRepository) => _foodTypeRepository = foodTypeRepository;

        public async Task<GetAllFoodTypeQueryResult> Handle(GetAllFoodTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _foodTypeRepository.GetAllAsync();
            if (result is not null || result.Count() > 0)
                return new GetAllFoodTypeQueryResult(result.ToList());
            return new GetAllFoodTypeQueryResult(new List<Domain.Entity.FoodType>());
        }
    }
}
