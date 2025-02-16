using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient
{
    public class CreateRecipeIngredientCommandHandler : IRequestHandler<CreateRecipeIngredientCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ISender _sender;
        private readonly ILogger<CreateRecipeIngredientCommandHandler> _logger;
        public CreateRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender, ILogger<CreateRecipeIngredientCommandHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
            _logger = logger;
        }

        public async Task Handle(CreateRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.IngredientId.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "IngredientId is invalide");
                }
                if (request.RecipeId.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "RecipeId is invalide");
                }
                List<GetRecipeIngredientByRecipeIdQueryResult> recipeIngredients = new();
                var ingredient = await _sender.Send(new GetIngredientByIdQuery(request.IngredientId));

                if (ingredient is not null)
                {
                    if (!request.RecipeId.IsNullOrEmpty())
                    {
                        recipeIngredients = await _sender.Send(new GetRecipeIngredientByRecipeIdQuery((Guid)request.RecipeId));
                        if (recipeIngredients.Any(rc => rc.Ingredient.Name == ingredient.Name))
                        {
                            throw new RecipeIngredientAlreadyExistException("Conflict", $"RecipeIngredien with ingredient Name {ingredient.Name} for Recipe {request.RecipeId}");
                        }
                    }
                    await _recipeIngredientRepository.AddAsync(new Domain.Entity.RecipeIngredient()
                    {
                        Id = Guid.NewGuid(),
                        IngredientId = request.IngredientId,
                        RecipeId = request.RecipeId,
                        Quantity = request.Quantity,
                        Unit = request.Unit

                    });
                    _logger.LogInformation("CreateRecipeIngredientCommandHandler : recipe ingredient created");
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
