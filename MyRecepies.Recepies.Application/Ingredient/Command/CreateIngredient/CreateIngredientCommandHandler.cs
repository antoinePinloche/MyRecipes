using MediatR;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;

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
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(request.Name));
            }
            //Checker si ingredient existe déja (recherche par nom)


            Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient() {Id = Guid.NewGuid(), FoodTypeId = request.FoodTypeId, Name = request.Name};
            await _ingredientRepository.AddAsync(ingredient);
        }
    }
}
