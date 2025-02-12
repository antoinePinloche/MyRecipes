using MediatR;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe
{
    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
    {
        private readonly IRecipesRepository _recipeRepository;
        private readonly ISender _sender;

        public DeleteRecipeCommandHandler(IRecipesRepository recipeRepository, ISender sender)
        {
            _recipeRepository = recipeRepository;
            _sender = sender;
        }

        public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Id.IsEmpty())
            {
                throw new Exception();
            }
            Domain.Entity.Recipe recipeFound = await _recipeRepository.GetAsync(request.Id);
            if (recipeFound is null)
            {
                throw new Exception();
            }

            await _sender.Send(new DeleteRecipeIngredientByRecipeIdCommand(request.Id));
            await _sender.Send(new DeleteInstructionByRecipeIdCommand(request.Id));
            recipeFound.Ingredients = null;
            recipeFound.Instructions = null;
            await _recipeRepository.RemoveAsync(recipeFound);
        }
    }
}
