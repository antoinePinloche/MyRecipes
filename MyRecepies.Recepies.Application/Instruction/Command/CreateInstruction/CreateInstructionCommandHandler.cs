using MediatR;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction
{
    public class CreateInstructionCommandHandler : IRequestHandler<CreateInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        public CreateInstructionCommandHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;
        public async Task Handle(CreateInstructionCommand request, CancellationToken cancellationToken)
        {
            if (request.StepInstruction.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "StepInstruction is invalide");
            }
            if (request.StepName.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "StepName is invalide");
            }
            if (request.RecipeId.IsNullOrEmpty())
            {
                throw new WrongParameterException("Invalide parameter", "RecipeId is invalide");
            }
            try
            {
                var instructionRecipe = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)request.RecipeId);
                if (instructionRecipe.Any(a => a.Step == request.Step))
                {
                    throw new InstructionAlreadyExisteException("Can't Create instruction step already Exist", $"Instruction {request.Step} already Exist");
                }
                Domain.Entity.Instruction instructionToAdd = new()
                {
                    Id = Guid.NewGuid(),
                    RecipeId = request.RecipeId,
                    Step = request.Step,
                    StepName = request.StepName,
                    StepInstruction = request.StepInstruction
                };
                await _instructionRepository.AddAsync(instructionToAdd);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
