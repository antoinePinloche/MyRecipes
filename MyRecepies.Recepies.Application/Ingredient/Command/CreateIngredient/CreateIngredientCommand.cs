using MediatR;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient
{
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
