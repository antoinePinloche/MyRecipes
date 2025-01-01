using MyRecipes.Recipes.Domain.Entity.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class Recipe
    {
        public Guid id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<RecipeIngredient> Ingredients { get; set; }
        public IEnumerable<Instruction> Instructions { get; set; }
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest {  get; set; }
    }
}
