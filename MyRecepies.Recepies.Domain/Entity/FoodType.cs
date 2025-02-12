using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class FoodType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Ingredient> Ingredients { get; set; }
    }
}
