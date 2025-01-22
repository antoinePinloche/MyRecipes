using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType
{
    public class GetAllFoodTypeQuery : IRequest<GetAllFoodTypeQueryResult>
    {
        public GetAllFoodTypeQuery() { }
    }
}
