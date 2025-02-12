using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId
{
    public class DeleteRecipeIngredientByRecipeIdCommandHandler : IRequestHandler<DeleteRecipeIngredientByRecipeIdCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        public DeleteRecipeIngredientByRecipeIdCommandHandler(IRecipeIngredientRepository recipeIngredientRepository)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
        }

        public async Task Handle(DeleteRecipeIngredientByRecipeIdCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Id.IsEmpty())
            {
                throw new Exception();
            }
            var RecipeIngredientList = await _recipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync(request.Id);

            if (!RecipeIngredientList.IsNullOrEmpty())
            {
                await _recipeIngredientRepository.RemoveRangeAsync(RecipeIngredientList);
            }
        }
    }
}
