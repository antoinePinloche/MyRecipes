using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    /// <summary>
    /// Query pour retourner toutes les instructions d'une recette
    /// <see cref="GetAllInstructionByRecipeIdQueryHandler"/>
    /// </summary>
    public class GetAllInstructionByRecipeIdQuery : IRequest<List<GetAllInstructionByRecipeIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetAllInstructionByRecipeIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
