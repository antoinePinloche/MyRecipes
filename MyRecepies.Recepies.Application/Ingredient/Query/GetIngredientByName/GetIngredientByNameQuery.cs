using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    public class GetIngredientByNameQuery : IRequest<GetIngredientByNameQueryResult>
    {
        public string Name { get; set; }

        public GetIngredientByNameQuery(string name)
        {
            Name = name;
        }
    }
}
