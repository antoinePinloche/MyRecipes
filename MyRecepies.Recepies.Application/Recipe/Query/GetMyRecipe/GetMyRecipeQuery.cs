using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetMyRecipe
{
    public class GetMyRecipeQuery: IRequest<List<GetMyRecipeQueryResult>>
    {
        public Guid Id { get; set; }

        public GetMyRecipeQuery(Guid guid) { Id = guid; }
    }
}
