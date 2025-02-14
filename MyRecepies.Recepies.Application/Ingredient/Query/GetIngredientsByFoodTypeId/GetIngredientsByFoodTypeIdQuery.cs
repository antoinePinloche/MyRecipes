using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQuery : IRequest<List<GetIngredientsByFoodTypeIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetIngredientsByFoodTypeIdQuery(Guid id) => Id = id;
    }
}
