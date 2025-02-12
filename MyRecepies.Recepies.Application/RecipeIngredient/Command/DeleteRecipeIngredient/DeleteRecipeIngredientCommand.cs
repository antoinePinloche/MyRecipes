using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient
{
    public class DeleteRecipeIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteRecipeIngredientCommand(Guid id) => this.Id = id;
    }
}
