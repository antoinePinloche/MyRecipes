using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById
{
    public class GetInstructionByIdQuery : IRequest<GetInstructionByIdQueryResult>
    {
        public Guid Id { get; set; }
        public GetInstructionByIdQuery(Guid id) => Id = id;
    }
}
