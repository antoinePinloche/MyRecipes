using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Command
{
    public sealed class DeleteInstructionByRecipeIdCommandTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<DeleteInstructionByRecipeIdCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteInstructionByRecipeIdCommand : WrongParameterException")]
        public async Task DeleteInstructionByRecipeIdCommandTest_WrongParameterExceptionAsync()
        {
            Guid guid = Guid.Empty;
            DeleteInstructionByRecipeIdCommand query = new DeleteInstructionByRecipeIdCommand(guid);
            DeleteInstructionByRecipeIdCommandHandler handler = new DeleteInstructionByRecipeIdCommandHandler(_instructionRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }
        [Fact]
        [Description("DeleteInstructionByRecipeIdCommand : InstructionNotFoundException")]
        public async Task DeleteInstructionByRecipeIdCommandTest_InstructionNotFoundExceptionAsync()
        {
            Guid guid = Guid.NewGuid();
            DeleteInstructionByRecipeIdCommand query = new DeleteInstructionByRecipeIdCommand(guid);
            DeleteInstructionByRecipeIdCommandHandler handler = new DeleteInstructionByRecipeIdCommandHandler(_instructionRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }
        [Fact]
        [Description("DeleteInstructionByRecipeIdCommand : InstructionNotFoundException")]
        public async void DeleteInstructionByRecipeIdCommandTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeGuid = Guid.NewGuid();
            DeleteInstructionByRecipeIdCommand query = new DeleteInstructionByRecipeIdCommand(guid);
            DeleteInstructionByRecipeIdCommandHandler handler = new DeleteInstructionByRecipeIdCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 0, StepInstruction = "", StepName = "préparation" };
            Domain.Entity.Instruction instruction2 = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 1, StepInstruction = "", StepName = "préparation" };
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() {instruction, instruction2 });
            _instructionRepository.Setup(x => x.RemoveRangeAsync(It.IsAny<List<Domain.Entity.Instruction>>()));
            await handler.Handle(query, _cancellationToken);
        }
    }
}
