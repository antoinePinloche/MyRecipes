using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction
{
    public class CreateListOfInstructionCommandHandler : IRequestHandler<CreateListOfInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        public CreateListOfInstructionCommandHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task Handle(CreateListOfInstructionCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new Exception();
            }
            if (request.Instructions.IsNullOrEmpty())
            {
                throw new Exception();
            }
            try
            {
                List<Domain.Entity.Instruction> instructionsToAdd = request.Instructions.Select(s =>
                new Domain.Entity.Instruction()
                {
                    Id = Guid.NewGuid(),
                    RecipeId = s.RecipeId,
                    Step = s.Step,
                    StepName = s.StepName,
                    StepInstruction = s.StepInstruction
                }).ToList();
                await _instructionRepository.AddRangeAsync(instructionsToAdd);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
