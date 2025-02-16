using MediatR;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;

        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository) => _ingredientRepository = ingredientRepository;

        public async Task Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            if (request.Name.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Name is invalide");
            }
            if (request.FoodTypeId.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "FoodTypeId is invalide");
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
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
