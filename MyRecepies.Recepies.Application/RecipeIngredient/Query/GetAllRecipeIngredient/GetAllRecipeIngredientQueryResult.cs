using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient
{
    public class GetAllRecipeIngredientQueryResult
    {
        public Guid Id { get; set; }
        public Guid? IngredientId { get; set; }
        public Domain.Entity.Ingredient? Ingredient { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public Guid? RecipeId { get; set; }
        //public Recipe? Recipe { get; set; }
        public GetAllRecipeIngredientQueryResult(Guid id, Guid? ingredientId, Domain.Entity.Ingredient? ingredient,
            double quantity, UnitOfMeasure unit, Guid? recipeId/*, Recipe? recipe*/)
        {
            Id = id;
            IngredientId = ingredientId;
            Ingredient = ingredient;
            Quantity = quantity;
            Unit = unit;
            RecipeId = recipeId;
            //Recipe = recipe;
        }
    }
}
