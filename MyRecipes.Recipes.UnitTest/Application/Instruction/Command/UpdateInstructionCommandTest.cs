using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.UpdateInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Command
{
    public sealed class UpdateInstructionCommandTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<UpdateInstructionCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("UpdateInstructionCommand : WrongParameterException id")]
        public void UpdateInstructionCommandTest_WrongParameterException_id()
        {
            Guid guid = Guid.Empty;
            int step = 0;
            string stepName = string.Empty;
            string stepDescription = string.Empty;
            UpdateInstructionCommand query = new UpdateInstructionCommand(guid, step, stepName, stepDescription);
            UpdateInstructionCommandHandler handler = new UpdateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateInstructionCommand : WrongParameterException stepName")]
        public void UpdateInstructionCommandTest_WrongParameterException_stepName()
        {
            Guid guid = Guid.NewGuid();
            int step = 0;
            string stepName = string.Empty;
            string stepDescription = string.Empty;
            UpdateInstructionCommand query = new UpdateInstructionCommand(guid, step, stepName, stepDescription);
            UpdateInstructionCommandHandler handler = new UpdateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateInstructionCommand : WrongParameterException stepDescription")]
        public void UpdateInstructionCommandTest_WrongParameterException_stepDescription()
        {
            Guid guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            UpdateInstructionCommand query = new UpdateInstructionCommand(guid, step, stepName, stepDescription);
            UpdateInstructionCommandHandler handler = new UpdateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateInstructionCommand : InstructionNotFoundException")]
        public async void UpdateInstructionCommandTest_InstructionNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            UpdateInstructionCommand query = new UpdateInstructionCommand(guid, step, stepName, stepDescription);
            UpdateInstructionCommandHandler handler = new UpdateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = guid,
                Step = step,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid()
            };
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction });
            await Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateInstructionCommand : InstructionAlreadyExisteException")]
        public async void UpdateInstructionCommandTest_InstructionAlreadyExisteException()
        {
            Guid guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            UpdateInstructionCommand query = new UpdateInstructionCommand(guid, step, stepName, stepDescription);
            UpdateInstructionCommandHandler handler = new UpdateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = guid,
                Step = step,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid()
            };
            Domain.Entity.Instruction instruction2 = new Domain.Entity.Instruction()
            {
                Id = Guid.NewGuid(),
                Step = step,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid()
            };
            _instructionRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(instruction);
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction2 });

            await Assert.ThrowsAsync<InstructionAlreadyExisteException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateInstructionCommand : Ok")]
        public async void UpdateInstructionCommandTest_OK()
        {
            Guid guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            UpdateInstructionCommand query = new UpdateInstructionCommand(guid, step, stepName, stepDescription);
            UpdateInstructionCommandHandler handler = new UpdateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = guid,
                Step = 1,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid(),
            };
            _instructionRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(instruction);
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction });
            await handler.Handle(query, _cancellationToken);
        }
    }
}
