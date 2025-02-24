using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, GetIngredientByIdQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        private readonly ILogger<GetIngredientByIdQueryHandler> _logger;
        public GetIngredientByIdQueryHandler(IIngredientRepository ingredienRepository, ILogger<GetIngredientByIdQueryHandler> logger)
        {
            _ingredienRepository = ingredienRepository;
            _logger = logger;
        }

        public async Task<GetIngredientByIdQueryResult> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "GetIngredientByIdQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                Domain.Entity.Ingredient ingredient = await _ingredienRepository.GetAsync(request.Id);
                if (ingredient is null)
                    throw new IngredientNotFoundException(
                        _logger,
                        nameof(Handle),
                        "GetIngredientByIdQueryHandler",
                        Constant.EXCEPTION.TITLE.CONFLICT,
                        $"Ingredient with ID {request.Id} already exist");
                _logger.LogInformation($"GetIngredientByIdQueryHandler : Ingredient {ingredient.Name} with ID {ingredient.Id} return");
                return new GetIngredientByIdQueryResult()
                {
                    Id = request.Id,
                    FoodTypeName = ingredient.FoodType.Name,
                    Name = ingredient.Name,
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
