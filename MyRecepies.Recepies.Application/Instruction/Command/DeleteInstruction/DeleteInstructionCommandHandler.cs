using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Constant;
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
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "DeleteInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var entity = await _instructionRepository.GetAsync(request.Id);
                if (entity is null)
                    throw new InstructionNotFoundException(_logger,
                        nameof(Handle),
                        "DeleteInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_KEY,
                        $"Instruction {request.Id} not found");
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
