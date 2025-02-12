using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe
{
    public class GetAllRecipeQuery : IRequest<List<GetAllRecipeQueryResult>>
    {
    }
}
