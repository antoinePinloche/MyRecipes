using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.UpdateInstruction
{
    public class UpdateInstructionCommandHandler : IRequestHandler<UpdateInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        public UpdateInstructionCommandHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task Handle(UpdateInstructionCommand request, CancellationToken cancellationToken)
        {
            var entityFound = await _instructionRepository.GetAsync(request.Id);
            if (entityFound is null)
            {
                throw new Exception();
            }
            var allInstruction = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)entityFound.RecipeId);
            if (entityFound.StepInstruction != request.StepInstruction)
                entityFound.StepInstruction = request.StepInstruction;
            if (entityFound.StepName != request.StepName)
                entityFound.StepName = request.StepName;
            if (!allInstruction.Any(w => w.Step == request.Step && w.Id != entityFound.Id))
                throw new Exception();
            entityFound.Step = request.Step;
            await _instructionRepository.UpdateAsync(entityFound);
            throw new NotImplementedException();
        }
    }
}
