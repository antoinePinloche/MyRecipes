using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById
{
    public class GetRecipeIngredientByIdQuery : IRequest<GetRecipeIngredientByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRecipeIngredientByIdQuery(Guid id) => Id = id;
    }
}
