using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById
{
    public class GetRecipeByIdQuery : IRequest<GetRecipeByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRecipeByIdQuery(Guid id) => Id = id;
    }
}
