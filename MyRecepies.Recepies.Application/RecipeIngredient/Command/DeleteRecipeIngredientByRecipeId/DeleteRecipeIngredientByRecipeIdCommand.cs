using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId
{
    public class DeleteRecipeIngredientByRecipeIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRecipeIngredientByRecipeIdCommand(Guid id) => Id = id;
    }
}
