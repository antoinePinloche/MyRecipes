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
        public List<Domain.Entity.Ingredient> ingredients {  get; set; }

        public GetAllIngredientQueryResult(List<Domain.Entity.Ingredient> ingredients)
        {
            this.ingredients = ingredients;
        }
    }
}
