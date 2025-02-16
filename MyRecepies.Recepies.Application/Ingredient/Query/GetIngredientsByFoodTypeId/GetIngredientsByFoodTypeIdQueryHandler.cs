using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQueryHandler : IRequestHandler<GetIngredientsByFoodTypeIdQuery, List<GetIngredientsByFoodTypeIdQueryResult>>
    {
        private readonly IIngredientRepository _ingredienRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        public GetIngredientsByFoodTypeIdQueryHandler(IIngredientRepository ingredienRepository, IFoodTypeRepository foodTypeRepository)
        {
            _ingredienRepository = ingredienRepository;
            _foodTypeRepository = foodTypeRepository;
        }

        public async Task<List<GetIngredientsByFoodTypeIdQueryResult>> Handle(GetIngredientsByFoodTypeIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            Domain.Entity.FoodType foodType = await _foodTypeRepository.GetAsync(request.Id);
            if (foodType is null)
            {
                throw new FoodTypeNotFoundException("Invalide key", $"FoodType not Found with Id {request.Id}");
            }
            List<Domain.Entity.Ingredient> ingredients = await _ingredienRepository.GetAllIngredientsByFoodTypeId(request.Id);

            if (!ingredients.IsNullOrEmpty())
            {
                return ingredients.Select(i =>
                    new GetIngredientsByFoodTypeIdQueryResult()
                    {
                        Id = i.Id,
                        FoodTypeName = i.FoodType.Name,
                        Name = i.FoodType.Name
                    }

                ).ToList();
            }
            return new List<GetIngredientsByFoodTypeIdQueryResult>();
        }
    }
}
