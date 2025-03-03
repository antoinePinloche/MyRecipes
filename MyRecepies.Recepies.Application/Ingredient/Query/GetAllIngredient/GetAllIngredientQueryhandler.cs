using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    /// <summary>
    /// Handler de la query <see cref="GetAllIngredientQuery"/>
    /// </summary>
    public class GetAllIngredientQueryHandler : IRequestHandler<GetAllIngredientQuery, List<GetAllIngredientQueryResult>>
    {
        private readonly IIngredientRepository _ingredienRepository;
        private readonly ILogger<GetAllIngredientQueryHandler> _logger;
        public GetAllIngredientQueryHandler(IIngredientRepository ingredienRepository, ILogger<GetAllIngredientQueryHandler> logger)
        {
            _ingredienRepository = ingredienRepository;
            _logger = logger;
        }

        public async Task<List<GetAllIngredientQueryResult>> Handle(GetAllIngredientQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entity.Ingredient> result = new List<Domain.Entity.Ingredient>();
            var entities = await _ingredienRepository.GetAllAsync();
            if (entities is not null)
            {
                _logger.LogInformation($"GetAllIngredientQueryHandler : Ingredient found return");
                return entities.Select(e =>
                    new GetAllIngredientQueryResult()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        FoodTypeInformation = new GetAllIngredientQueryResult.FoodType()
                        {
                            Id = e.FoodType.Id,
                            Name = e.FoodType.Name
                        }
                    }

                ).ToList();
            }
            _logger.LogInformation($"GetAllIngredientQueryHandler : complete without error and no ingredient to return");
            return new List<GetAllIngredientQueryResult>();
        }
    }
}
