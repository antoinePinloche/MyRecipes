using MediatR;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    public class GetAllIngredientQuery : IRequest<GetAllIngredientQueryResult>
    {
        public GetAllIngredientQuery() { }
    }
}
