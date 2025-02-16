using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    public class GetIngredientByNameQueryHandler : IRequestHandler<GetIngredientByNameQuery, GetIngredientByNameQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        private readonly ILogger<GetIngredientByNameQueryHandler> _logger;
        public GetIngredientByNameQueryHandler(IIngredientRepository ingredienRepository, ILogger<GetIngredientByNameQueryHandler> logger)
        {
            _ingredienRepository = ingredienRepository;
            _logger = logger;
        }
        public async Task<GetIngredientByNameQueryResult> Handle(GetIngredientByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Name.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Name is invalide");
                }
                var entity = await _ingredienRepository.HasIngredient(request.Name);
                if (entity is null)
                    throw new IngredientAlreadyExistException("Conflict", $"Ingredient with Name {request.Name} already exist");
                _logger.LogInformation($"GetIngredientByNameQueryHandler : Ingredient {entity.Name} return");
                return new GetIngredientByNameQueryResult() 
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    FoodTypeName = entity.FoodType.Name
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
