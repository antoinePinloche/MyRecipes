using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    public class GetAllInstructionByRecipeIdQueryHandler : IRequestHandler<GetAllInstructionByRecipeIdQuery, List<GetAllInstructionByRecipeIdQueryResult>>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IRecipesRepository _recipesRepository;
        public GetAllInstructionByRecipeIdQueryHandler(IInstructionRepository instructionRepository, IRecipesRepository recipesRepository)
        {
            _recipesRepository = recipesRepository;
            _instructionRepository = instructionRepository;
        }

        public async Task<List<GetAllInstructionByRecipeIdQueryResult>> Handle(GetAllInstructionByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            var recipeFound = await _recipesRepository.GetAsync(request.Id);
            if (recipeFound is null)
            {
                throw new RecipeNotFoundException("Invalide Key", $"Recipe notfound with ID {request.Id}");
            }
            var entityFound = await _instructionRepository.GetAllInstructionByRecipeIdAsync(request.Id);
            if (entityFound.IsNullOrEmpty())
                throw new InstructionNotFoundException("Invalide Key", $"Instruction for recipe {request.Id} not found");
            return entityFound.OrderBy(ob => ob.Step).Select(s =>
                new GetAllInstructionByRecipeIdQueryResult(
                    s.Id,
                    s.Step,
                    s.StepName,
                    s.StepInstruction
                    )
            ).ToList();
        }
    }
}
