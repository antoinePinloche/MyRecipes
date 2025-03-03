using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    /// <summary>
    /// Query pour retourner l'Ingredien correspondant a nom
    /// <see cref="GetIngredientByNameQueryHandler"/>
    /// </summary>
    public class GetIngredientByNameQuery : IRequest<GetIngredientByNameQueryResult>
    {
        public string Name { get; set; }

        public GetIngredientByNameQuery(string name)
        {
            Name = name;
        }
    }
}
