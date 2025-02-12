using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById
{
    public class UpdateFoodTypeByIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateFoodTypeByIdCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
