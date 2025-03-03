using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient
{
    /// <summary>
    /// Command pour crée une RecipeIngredient
    /// <see cref="CreateRecipeIngredientCommandHandler"/>
    /// </summary>
    public class CreateRecipeIngredientCommand : IRequest
    {
        public Guid IngredientId { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public Guid? RecipeId { get; set; }
    }
}
