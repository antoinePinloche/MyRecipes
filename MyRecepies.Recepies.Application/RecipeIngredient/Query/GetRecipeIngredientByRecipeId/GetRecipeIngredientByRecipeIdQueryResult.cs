using MyRecipes.Recipes.Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId
{
    public class GetRecipeIngredientByRecipeIdQueryResult
    {
        public Guid Id { get; set; }
        public Guid? IngredientId { get; set; }
        public Domain.Entity.Ingredient? Ingredient { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public GetRecipeIngredientByRecipeIdQueryResult(Guid id, Guid? ingredientId, Domain.Entity.Ingredient? ingredient,
            double quantity, UnitOfMeasure unit)
        {
            Id = id;
            IngredientId = ingredientId;
            Ingredient = ingredient;
            Quantity = quantity;
            Unit = unit;
        }
    }
}
