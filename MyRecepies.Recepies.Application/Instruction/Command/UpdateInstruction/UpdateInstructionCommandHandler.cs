using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
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
            if (request.Id.IsEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "Id is invalide");
            }
            if (request.StepInstruction.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "StepInstruction is invalide");
            }
            if (request.StepName.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "StepName is invalide");
            }
            var entityFound = await _instructionRepository.GetAsync(request.Id);
            if (entityFound is null)
            {
                throw new InstructionNotFoundException("Invalide Key", $"Instruction {request.Id} not found");
            }
            var allInstruction = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)entityFound.RecipeId);
            if (entityFound.StepInstruction != request.StepInstruction)
                entityFound.StepInstruction = request.StepInstruction;
            if (entityFound.StepName != request.StepName)
                entityFound.StepName = request.StepName;
            //refaire cette exeption et la condition car ca merde bien la 
            if (allInstruction.Any(w => w.Step == request.Step && w.Id != entityFound.Id))
                throw new InstructionAlreadyExisteException("Can't update step already Exist", $"Instruction {request.Id} already Exist");
            entityFound.Step = request.Step;
            await _instructionRepository.UpdateAsync(entityFound);
        }
    }
}
