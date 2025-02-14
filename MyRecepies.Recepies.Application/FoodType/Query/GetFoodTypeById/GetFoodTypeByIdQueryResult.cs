using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    public class GetFoodTypeByIdQueryResult
    {
        public Domain.Entity.FoodType FoodType { get; set; }

        public GetFoodTypeByIdQueryResult(Domain.Entity.FoodType ft) {
            FoodType = ft;
        }
    }
}
