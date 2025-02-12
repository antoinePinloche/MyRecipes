using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient
{
    public class DeleteIngredientCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteIngredientCommand(Guid id) => Id = id;
    }
}
