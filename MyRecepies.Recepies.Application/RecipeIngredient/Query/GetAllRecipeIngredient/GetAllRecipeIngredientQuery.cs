using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient
{
    public class GetAllRecipeIngredientQuery : IRequest<List<GetAllRecipeIngredientQueryResult>>
    {
    }
}
