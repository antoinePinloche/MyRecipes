using MediatR;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
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
            }
            
        }
    }
}
