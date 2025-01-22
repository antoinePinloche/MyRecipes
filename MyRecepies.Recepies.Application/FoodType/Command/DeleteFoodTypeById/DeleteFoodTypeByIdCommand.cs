using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById
{
    public class DeleteFoodTypeByIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteFoodTypeByIdCommand(Guid id) => Id = id;

    }
}
