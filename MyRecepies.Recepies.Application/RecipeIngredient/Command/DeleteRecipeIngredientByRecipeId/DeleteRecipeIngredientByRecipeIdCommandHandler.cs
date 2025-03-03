using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId
{
    /// <summary>
    /// handler de la command <see cref="DeleteRecipeIngredientByRecipeIdCommand"/>
    /// </summary>
    public class DeleteRecipeIngredientByRecipeIdCommandHandler : IRequestHandler<DeleteRecipeIngredientByRecipeIdCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ILogger<DeleteRecipeIngredientByRecipeIdCommandHandler> _logger;
        public DeleteRecipeIngredientByRecipeIdCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, ILogger<DeleteRecipeIngredientByRecipeIdCommandHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteRecipeIngredientByRecipeIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "DeleteRecipeIngredientByRecipeIdCommandHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var RecipeIngredientList = await _recipeIngredientRepository.GetAllRecipeIngredientByRecipeIdlAsync(request.Id);
                if (!RecipeIngredientList.IsNullOrEmpty())
                {
                    await _recipeIngredientRepository.RemoveRangeAsync(RecipeIngredientList);
                    _logger.LogInformation($"DeleteRecipeIngredientByRecipeIdCommandHandler : recipe ingredient delete for recipe {request.Id}");
                }
                else
                {
                    _logger.LogInformation($"DeleteRecipeIngredientByRecipeIdCommandHandler : Finish without error because no recipeIngredient to delete");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
