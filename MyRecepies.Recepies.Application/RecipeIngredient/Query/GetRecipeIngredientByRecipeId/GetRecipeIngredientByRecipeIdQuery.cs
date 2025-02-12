using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId
{
    public class GetRecipeIngredientByRecipeIdQuery : IRequest<List<GetRecipeIngredientByRecipeIdQueryResult>>
    {
        public Guid Id { get; set; }
        public GetRecipeIngredientByRecipeIdQuery(Guid id) => Id = id;
    }
}
