using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType
{
    public class CreateFoodTypeCommand : IRequest
    {
        public string Name { get; set; }

        public CreateFoodTypeCommand(string name) => Name = name;
    }
}
