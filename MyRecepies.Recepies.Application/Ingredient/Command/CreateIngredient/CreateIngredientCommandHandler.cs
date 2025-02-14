using MediatR;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;

namespace MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;

        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository) => _ingredientRepository = ingredientRepository;

        public async Task Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new Exception(/*nameof(CreateIngredientCommandHandler), nameof(request),*/ "wrong parameter");
            }
            if (request.Name.IsNullOrEmpty())
            {
                throw new Exception(/*nameof(CreateIngredientCommandHandler), nameof(request.Name), */"wrong parameter");
            }
            try
            {
                var entity = await _ingredientRepository.HasIngredient(request.Name);
                if (entity is not null)
                {
                    throw new IngredientAlreadyExistException("Conflict", $"Ingredient with Name {request.Name} already exist");
                }
                Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient() { Id = Guid.NewGuid(), FoodTypeId = request.FoodTypeId, Name = request.Name };
                await _ingredientRepository.AddAsync(ingredient);
            }
            catch (IngredientAlreadyExistException ex)
            {
                throw new IngredientAlreadyExistException(ex.Error, ex.Message);
            }

        }
    }
}
