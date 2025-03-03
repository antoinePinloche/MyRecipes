using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    /// <summary>
    /// Query pour retourner l'Ingredien correspondant a ID
    /// <see cref="GetIngredientByIdQueryHandler"/>
    /// </summary>
    public class GetIngredientByIdQuery : IRequest<GetIngredientByIdQueryResult>
    {
        public Guid Id { get; set; }
        public GetIngredientByIdQuery(Guid id) => Id = id;
    }
}
