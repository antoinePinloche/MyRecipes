using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    /// <summary>
    /// Handler de la query <see cref="GetIngredientByNameQuery"/>
    /// </summary>
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
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "GetIngredientByNameQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.NAME);
                }
                var entity = await _ingredienRepository.HasIngredient(request.Name);
                if (entity is null)
                    throw new IngredientNotFoundException(
                        _logger,
                        nameof(Handle),
                        "GetIngredientByNameQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_KEY,
                        $"Ingredient with Name {request.Name} not found");
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
