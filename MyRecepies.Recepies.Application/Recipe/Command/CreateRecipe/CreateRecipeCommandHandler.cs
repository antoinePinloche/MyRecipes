using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ILogger<CreateRecipeCommandHandler> _logger;
        public CreateRecipeCommandHandler(IRecipesRepository recipeRepository, ILogger<CreateRecipeCommandHandler> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }
        public async Task Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Name.IsNullOrEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Name is invalide");
                }
                Domain.Entity.Recipe entityToAdd = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    RecipyDifficulty = request.RecipyDifficulty,
                    TimeToPrepareRecipe = request.TimeToPrepareRecipe,
                    NbGuest = request.NbGuest
                };
                await _recipeRepository.AddAsync(entityToAdd);
                _logger.LogInformation($"CreateRecipeCommandHandler : recipe {request.Name} create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
