using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<CreateIngredientCommandHandler> _logger;
        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository, ILogger<CreateIngredientCommandHandler> logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        public async Task Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Name.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Name is invalide");
                }
                if (request.FoodTypeId.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "FoodTypeId is invalide");
                }
                var entity = await _ingredientRepository.HasIngredient(request.Name);
                if (entity is not null)
                {
                    throw new IngredientAlreadyExistException("Conflict", $"Ingredient with Name {request.Name} already exist");
                }
                Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient() { Id = Guid.NewGuid(), FoodTypeId = request.FoodTypeId, Name = request.Name };
                await _ingredientRepository.AddAsync(ingredient);
                _logger.LogInformation($"CreateIngredientCommand : Ingredient {request.Name} create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
