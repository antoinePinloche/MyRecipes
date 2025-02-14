using MediatR;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    public class GetIngredientByNameQueryHandler : IRequestHandler<GetIngredientByNameQuery, GetIngredientByNameQueryResult>
    {
        private readonly IIngredientRepository _ingredienRepository;
        public GetIngredientByNameQueryHandler(IIngredientRepository ingredienRepository) => _ingredienRepository = ingredienRepository;
        public async Task<GetIngredientByNameQueryResult> Handle(GetIngredientByNameQuery request, CancellationToken cancellationToken)
        {
            if (request.Name.IsNullOrEmpty())
            {

            }
            var entity = await _ingredienRepository.HasIngredient(request.Name);
            if (entity is null)
                throw new IngredientAlreadyExistException("Conflict", $"Ingredient with Name {request.Name} already exist");
            return new GetIngredientByNameQueryResult() 
            {
                Id = entity.Id,
                Name = entity.Name,
                FoodTypeName = entity.FoodType.Name
            };
        }
    }
}
