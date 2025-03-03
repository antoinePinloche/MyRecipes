using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Web.API.Models.Class.RecipeIngredient.Model
{
    public class UpdateRecipeIngredientModel
    {
        public Guid IngredientId { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public Guid? RecipeId { get; set; }
    }
}
