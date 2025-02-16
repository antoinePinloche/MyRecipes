using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction
{
    public class DeleteInstructionCommandHandler : IRequestHandler<DeleteInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<DeleteInstructionCommandHandler> _logger;
        public DeleteInstructionCommandHandler(IInstructionRepository instructionRepository, ILogger<DeleteInstructionCommandHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteInstructionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                var entity = await _instructionRepository.GetAsync(request.Id);
                if (entity is null)
                    throw new InstructionNotFoundException("Invalide Key", $"Instruction {request.Id} not found");
                await _instructionRepository.RemoveAsync(entity);
                _logger.LogInformation($"DeleteInstructionCommandHandler : Instruction {request.Id} delete");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ;
            }
        }
    }
}
