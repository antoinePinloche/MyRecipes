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
            if (await _ingredientRepository.HasIngredient(request.Name))
            {
                throw new Exception("Ingredient already Exist");
            }
            Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient() {Id = Guid.NewGuid(), FoodTypeId = request.FoodTypeId, Name = request.Name};
            await _ingredientRepository.AddAsync(ingredient);
        }
    }
}
