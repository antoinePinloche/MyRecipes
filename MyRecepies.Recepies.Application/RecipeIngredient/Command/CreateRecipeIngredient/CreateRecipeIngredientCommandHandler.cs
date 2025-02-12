using MediatR;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient
{
    public class CreateRecipeIngredientCommandHandler : IRequestHandler<CreateRecipeIngredientCommand>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ISender _sender;
        public CreateRecipeIngredientCommandHandler(IRecipeIngredientRepository recipeIngredientRepository, ISender sender)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _sender = sender;
        }

        public async Task Handle(CreateRecipeIngredientCommand request, CancellationToken cancellationToken)
        {
            //Check a faire
            var ingredient = await _sender.Send(new GetIngredientByIdQuery(request.IngredientId));
            if (ingredient is not null)
            {
                await _recipeIngredientRepository.AddAsync(new Domain.Entity.RecipeIngredient()
                {
                    Id = Guid.NewGuid(),
                    IngredientId = request.IngredientId,
                    //RecipeId = null,
                    Quantity = request.Quantity,
                    Unit = request.Unit

                });
            }

        }
    }
}
