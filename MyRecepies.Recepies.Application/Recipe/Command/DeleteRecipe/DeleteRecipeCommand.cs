using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe
{
    public class DeleteRecipeCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRecipeCommand(Guid id) => Id = id;
    }
}
