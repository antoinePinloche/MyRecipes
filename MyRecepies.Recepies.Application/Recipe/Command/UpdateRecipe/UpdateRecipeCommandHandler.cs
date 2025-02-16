using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Command.UpdateRecipe
{
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ISender _sender;
        public UpdateRecipeCommandHandler(IRecipesRepository recipeRepository, ISender sender)
        {
            _recipeRepository = recipeRepository;
            _sender = sender;
        }

        public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            if (request.Name.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Name is invalide");
            }
            var entity = await _recipeRepository.GetAsync(request.Id);
            if (entity is null)
            {
                throw new RecipeNotFoundException("invalide key", $"Recipe with Id {request.Id} not found");
            }
            if (!entity.Name.IsNullOrEmpty() && entity.Name != request.Name)
                entity.Name = request.Name;
            entity.RecipyDifficulty = request.RecipyDifficulty;
            entity.NbGuest = request.NbGuest;
            entity.TimeToPrepareRecipe = request.TimeToPrepareRecipe;
            try
            {
                await _recipeRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
