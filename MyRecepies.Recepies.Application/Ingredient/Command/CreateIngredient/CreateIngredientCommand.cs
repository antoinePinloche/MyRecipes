using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient
{
    /// <summary>
    /// Command pour crée un Ingredient
    /// <see cref="CreateIngredientCommandHandler"/>
    /// </summary>
    public class CreateIngredientCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid FoodTypeId { get; set; }
        public CreateIngredientCommand(string name, Guid foodTypeId)
        {
            Name = name;
            FoodTypeId = foodTypeId;
        }
    }
}
