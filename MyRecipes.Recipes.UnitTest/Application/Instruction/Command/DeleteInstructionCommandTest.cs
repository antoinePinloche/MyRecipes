using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Command
{
    public class DeleteInstructionCommandTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<DeleteInstructionCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteInstructionCommand : WrongParameterException")]
        public void DeleteInstructionCommandTest_WrongParameterException()
        {
            Guid guid = Guid.Empty;
            DeleteInstructionCommand query = new DeleteInstructionCommand(guid);
            DeleteInstructionCommandHandler handler = new DeleteInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteInstructionCommand : InstructionNotFoundException")]
        public void DeleteInstructionCommandTest_InstructionNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            DeleteInstructionCommand query = new DeleteInstructionCommand(guid);
            DeleteInstructionCommandHandler handler = new DeleteInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
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
