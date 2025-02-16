using MyRecipes.Recipes.Domain.Entity.Enum;

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
