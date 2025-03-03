using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Web.API.Models.Class.Ingredient.Response;

namespace MyRecipes.Web.API.Models.Class.RecipeIngredient.Response
{
    public class RecipeIngredientResponse
    {
        public Guid Id { get; set; }
        public IngredientResponse? Ingredient { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
    }
}
