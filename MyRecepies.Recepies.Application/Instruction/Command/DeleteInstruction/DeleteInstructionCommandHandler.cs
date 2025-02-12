using MediatR;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction
{
    public class DeleteInstructionCommandHandler : IRequestHandler<DeleteInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        public DeleteInstructionCommandHandler(IInstructionRepository instructionRepository) => _instructionRepository = instructionRepository;

        public async Task Handle(DeleteInstructionCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new Exception();
            var entity = await _instructionRepository.GetAsync(request.Id);
            if (entity is null)
                throw new Exception();
            try
            {
                await _instructionRepository.RemoveAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
