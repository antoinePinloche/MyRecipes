using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient
{
    public class UpdateRecipeIngredientCommandHandler : IRequestHandler<UpdateRecipeIngredientCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<UpdateRecipeIngredientCommandHandler> _logger;
        public UpdateRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, IIngredientRepository ingredientRepository, IRecipesRepository recipesRepository, ILogger<UpdateRecipeIngredientCommandHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _ingredientRepository = ingredientRepository;
            _recipesRepository = recipesRepository;
            _logger = logger;
        }
        public async Task Handle(UpdateRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                if (request.RecipeId.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.RECIPE_ID);
                }
                if (request.IngredientId.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "UpdateRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.INGREDIENT_ID);
                }
                var riFound = await _recipeIngredientRepository.GetAsync(request.Id);
                if (riFound is null)
                {
                    throw new RecipeIngredientNotFoundException(_logger,
                        nameof(Handle),
                        "UpdateRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Ingredient Recipe with Id {request.Id} not found");
                }

                var recipe = await _recipesRepository.GetAsync((Guid)request.RecipeId);
                if (recipe is null)
                {
                    throw new RecipeNotFoundException(_logger,
                        nameof(Handle),
                        "UpdateRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"Recipe with ID {request.RecipeId} not found");
                }
                var recipeIngredients = await _recipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync((Guid)request.RecipeId);

                if (recipeIngredients is not null && recipeIngredients.Any(ri => ri.IngredientId == request.IngredientId))
                {
                    throw new RecipeIngredientAlreadyExistException(_logger,
                        nameof(Handle),
                        "UpdateRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.CONFLICT,
                        $"Recipe with ingredient {request.IngredientId} already exist");
                }

                if (riFound.IngredientId != request.IngredientId)
                {
                    riFound.IngredientId = request.IngredientId;
                    var ingredientFound = await _ingredientRepository.GetAsync(request.IngredientId);
                    riFound.Ingredient = ingredientFound;
                }

                if (riFound.RecipeId != request.RecipeId)
                    riFound.RecipeId = request.RecipeId;
                riFound.Unit = request.Unit;
                riFound.Quantity = request.Quantity;
                await _recipeIngredientRepository.UpdateAsync(riFound);
                _logger.LogInformation($"UpdateRecipeIngredientCommandHandler : recipe ingredient {request.Id} update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
