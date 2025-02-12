using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Web.API.Models.Class.RecipeIngredient
{
    public class CreateRecipeIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public Guid? RecipeId { get; set; }
    }
}
