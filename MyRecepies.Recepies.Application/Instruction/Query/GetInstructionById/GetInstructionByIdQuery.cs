using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById
{
    /// <summary>
    /// Query pour retourner l'instruction par son Id
    /// <see cref="GetInstructionByIdQueryHandler"/>
    /// </summary>
    public class GetInstructionByIdQuery : IRequest<GetInstructionByIdQueryResult>
    {
        public Guid Id { get; set; }
        public GetInstructionByIdQuery(Guid id) => Id = id;
    }
}
