using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
