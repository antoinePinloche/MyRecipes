using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId;
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
    public sealed class CreateInstructionCommandTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<CreateInstructionCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CreateInstructionCommand : WrongParameterException id")]
        public void CreateInstructionCommandTest_WrongParameterException_id()
        {
            Guid? guid = Guid.Empty;
            int step = 0;
            string stepName = string.Empty;
            string stepDescription = string.Empty;
            CreateInstructionCommand query = new CreateInstructionCommand(guid, step, stepName, stepDescription);
            CreateInstructionCommandHandler handler = new CreateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateInstructionCommand : WrongParameterException stepName")]
        public void CreateInstructionCommandTest_WrongParameterException_stepName()
        {
            Guid? guid = Guid.NewGuid();
            int step = 0;
            string stepName = string.Empty;
            string stepDescription = string.Empty;
            CreateInstructionCommand query = new CreateInstructionCommand(guid, step, stepName, stepDescription);
            CreateInstructionCommandHandler handler = new CreateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateInstructionCommand : WrongParameterException stepDescription")]
        public void CreateInstructionCommandTest_WrongParameterException_stepDescription()
        {
            Guid? guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            CreateInstructionCommand query = new CreateInstructionCommand(guid, step, stepName, stepDescription);
            CreateInstructionCommandHandler handler = new CreateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateInstructionCommand : InstructionAlreadyExisteException")]
        public async void CreateInstructionCommandTest_InstructionAlreadyExisteException()
        {
            Guid? guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            CreateInstructionCommand query = new CreateInstructionCommand(guid, step, stepName, stepDescription);
            CreateInstructionCommandHandler handler = new CreateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = Guid.NewGuid(),
                Step = step,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid(),
            };
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>(){ instruction});
            await Assert.ThrowsAsync<InstructionAlreadyExisteException>(async () => await handler.Handle(query, _cancellationToken));
        }
        [Fact]
        [Description("CreateInstructionCommand : Ok")]
        public async void CreateInstructionCommandTest_OK()
        {
            Guid? guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            CreateInstructionCommand query = new CreateInstructionCommand(guid, step, stepName, stepDescription);
            CreateInstructionCommandHandler handler = new CreateInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = Guid.NewGuid(),
                Step = 1,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid(),
            };
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction });
            await handler.Handle(query, _cancellationToken);
        }
    }
}
