using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using MyRecipes.Transverse.Exception;

namespace MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand>
    {
        private readonly IRecipesRepository _recipeRepository;

        public CreateRecipeCommandHandler(IRecipesRepository recipeRepository) => _recipeRepository = recipeRepository;

        public async Task Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
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
            try
            {
                await _recipeRepository.AddAsync(entityToAdd);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
