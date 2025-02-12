using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
    public class GetRecipeByNameQuery : IRequest<List<GetRecipeByNameQueryResult>>
    {
        public string Name { get; set; }
        public GetRecipeByNameQuery(string name) => Name = name;
    }
}
