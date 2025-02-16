using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public DeleteIngredientCommandHandler(IIngredientRepository ingredientRepository) => _ingredientRepository = ingredientRepository;

        public async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            var ingredient = await _ingredientRepository.GetAsync(request.Id);
            if (ingredient is null)
            {
                throw new IngredientNotFoundException("NotFound", $"Ingredient not found {request.Id}");
            }
            await _ingredientRepository.RemoveAsync(ingredient);
        }
    }
}
