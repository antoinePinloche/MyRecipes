using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    public class GetAllInstructionByRecipeIdQuery : IRequest<List<GetAllInstructionByRecipeIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetAllInstructionByRecipeIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
