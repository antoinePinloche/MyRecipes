using MediatR;

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
