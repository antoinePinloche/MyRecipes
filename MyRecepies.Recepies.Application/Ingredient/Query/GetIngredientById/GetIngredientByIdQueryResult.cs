using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    public class GetIngredientByIdQueryResult
    {
        public string FoodTypeName { get; set; }
        public Ingredient IngredientFound {  get; set; } 
        public class Ingredient
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string FoodTypeName { get; set; }

            public Ingredient(Guid id, string name, string foodTypeName)
            {
                Id = id;
                Name = name;
                FoodTypeName = foodTypeName;
            }
        }
    }
}
