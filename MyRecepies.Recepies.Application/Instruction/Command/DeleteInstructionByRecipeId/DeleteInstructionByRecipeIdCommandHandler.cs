using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId
{
    public class DeleteInstructionByRecipeIdCommandHandler : IRequestHandler<DeleteInstructionByRecipeIdCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        public DeleteInstructionByRecipeIdCommandHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task Handle(DeleteInstructionByRecipeIdCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Id.IsEmpty())
            {
                throw new Exception();
            }
            var RecipeIngredientList = await _instructionRepository.GetAllInstructionByRecipeIdAsync(request.Id);

            if (!RecipeIngredientList.IsNullOrEmpty())
            {
                await _instructionRepository.RemoveRangeAsync(RecipeIngredientList);
            }
            throw new InstructionNotFoundException("Invalide Key", $"Instructions for Recipe {request.Id} not found");
        }
    }
}
