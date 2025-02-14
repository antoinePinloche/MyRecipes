using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    public class GetAllIngredientQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public FoodType FoodTypeInformation { get; set; }

        public class FoodType
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
