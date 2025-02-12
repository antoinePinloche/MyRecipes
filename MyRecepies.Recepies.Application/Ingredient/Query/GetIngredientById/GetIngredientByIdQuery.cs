using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<GetIngredientByIdQueryResult>
    {
        public Guid Id { get; set; }
        public GetIngredientByIdQuery(Guid id) => Id = id;
    }
}
