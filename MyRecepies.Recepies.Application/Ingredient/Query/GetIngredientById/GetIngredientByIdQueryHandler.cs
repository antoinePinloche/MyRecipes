using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, GetIngredientByIdQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        public GetIngredientByIdQueryHandler(IIngredientRepository ingredienRepository) => _ingredienRepository = ingredienRepository;

        public async Task<GetIngredientByIdQueryResult> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entity.Ingredient ingredient = await _ingredienRepository.GetAsync(request.Id);
            if (ingredient is null)
                throw new IngredientNotFoundException("Conflict", $"Ingredient with ID {request.Id} already exist");
            return new GetIngredientByIdQueryResult()
            {
                Id = request.Id,
                FoodTypeName = ingredient.FoodType.Name,
                Name = ingredient.Name,
            };
        }
    }
}
