using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient
{
    public class DeleteRecipeIngredientCommandHandler : IRequestHandler<DeleteRecipeIngredientCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ISender _sender;
        private readonly ILogger<DeleteRecipeIngredientCommandHandler> _logger;
        public DeleteRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender, ILogger<DeleteRecipeIngredientCommandHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(DeleteRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "DeleteRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var recipeIngredientFound = await _sender.Send(new GetRecipeIngredientByIdQuery(request.Id));
                if (recipeIngredientFound is null)
                {
                    throw new RecipeIngredientNotFoundException(_logger,
                        nameof(Handle),
                        "DeleteRecipeIngredientCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND, $"RecipeIngredient not found with Id {request.Id}");
                }
                Domain.Entity.RecipeIngredient recipeIngredientToDelete = new()
                {
                    Id = recipeIngredientFound.Id,
                    IngredientId = recipeIngredientFound.IngredientId,
                    Ingredient = recipeIngredientFound.Ingredient,
                    Quantity = recipeIngredientFound.Quantity,
                    Unit = recipeIngredientFound.Unit,
                    RecipeId = recipeIngredientFound.RecipeId
                };
                await _recipeIngredientRepository.RemoveAsync(recipeIngredientToDelete);
                _logger.LogInformation($"DeleteRecipeIngredientCommandHandler : recipe ingredient {request.Id} delete");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ;
            }
        }
    }
}
