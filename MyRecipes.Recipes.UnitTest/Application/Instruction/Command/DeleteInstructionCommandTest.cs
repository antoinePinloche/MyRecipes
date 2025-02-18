using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Command
{
    public class DeleteInstructionCommandTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<DeleteInstructionCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteInstructionCommand : WrongParameterException")]
        public async Task DeleteInstructionCommandTest_WrongParameterExceptionAsync()
        {
            Guid guid = Guid.Empty;
            DeleteInstructionCommand query = new DeleteInstructionCommand(guid);
            DeleteInstructionCommandHandler handler = new DeleteInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteInstructionCommand : InstructionNotFoundException")]
        public async Task DeleteInstructionCommandTest_InstructionNotFoundExceptionAsync()
        {
            Guid guid = Guid.NewGuid();
            DeleteInstructionCommand query = new DeleteInstructionCommand(guid);
            DeleteInstructionCommandHandler handler = new DeleteInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteInstructionCommand : Ok")]
        public async void DeleteInstructionCommandTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeGuid = Guid.NewGuid();
            DeleteInstructionCommand query = new DeleteInstructionCommand(guid);
            DeleteInstructionCommandHandler handler = new DeleteInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 0, StepInstruction = "", StepName = "préparation" };
            _instructionRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(instruction);
            _instructionRepository.Setup(x => x.RemoveAsync(It.IsAny<Domain.Entity.Instruction>()));
            await handler.Handle(query, _cancellationToken);
        }
    }
}
