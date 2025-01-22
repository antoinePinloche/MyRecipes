using MyRecipes.Recipes.Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid FoodTypeId { get; set; }
        public FoodType FoodType { get; set; }

    }
}
