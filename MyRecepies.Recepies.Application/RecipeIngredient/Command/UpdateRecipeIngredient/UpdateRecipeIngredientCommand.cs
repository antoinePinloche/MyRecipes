using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient
{
    public class UpdateRecipeIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public Guid? RecipeId { get; set; }

        public UpdateRecipeIngredientCommand(Guid id, Guid ingredientId, double quantity, UnitOfMeasure unitOfMeasure, Guid? recipeId)
        {
            Id = id;
            IngredientId = ingredientId;
            Quantity = quantity;
            Unit = unitOfMeasure;
            RecipeId = recipeId;
        }
    }
}
