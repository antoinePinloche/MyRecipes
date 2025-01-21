using MyRecipes.Recipes.Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class RecipeIngredient
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit {  get; set; }
        public Guid? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
