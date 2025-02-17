using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction;
using MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction;
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
    public sealed class CreateListOfInstructionCommandTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<CreateListOfInstructionCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CreateListOfInstructionCommand : WrongParameterException id")]
        public async void CreateListOfInstructionCommandTest_WrongParameterException_id()
        {
            CreateListOfInstructionCommand query = new CreateListOfInstructionCommand(null);
            CreateListOfInstructionCommandHandler handler = new CreateListOfInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateListOfInstructionCommand : WrongParameterException id")]
        public async void CreateListOfInstructionCommandTest_WrongParameterException_multipleStepDuplication()
        {
            Guid recipeGuid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            CreateListOfInstructionCommand query = new CreateListOfInstructionCommand(new List<CreateListOfInstructionCommand.Instruction>()
            {
                new CreateListOfInstructionCommand.Instruction(recipeGuid, 0, stepName, stepDescription),
                new CreateListOfInstructionCommand.Instruction(recipeGuid, 0, "stepName2", stepDescription),
                new CreateListOfInstructionCommand.Instruction(recipeGuid, 1, "stepName3", stepDescription)
            });

            CreateListOfInstructionCommandHandler handler = new CreateListOfInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateListOfInstructionCommand : InstructionAlreadyExisteException stepName")]
        public async void CreateListOfInstructionCommandTest_InstructionAlreadyExisteException()
        {
            Guid recipeGuid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            CreateListOfInstructionCommand query = new CreateListOfInstructionCommand(new List<CreateListOfInstructionCommand.Instruction>()
            {
                new CreateListOfInstructionCommand.Instruction(recipeGuid, 0, stepName, stepDescription)
            });
            CreateListOfInstructionCommandHandler handler = new CreateListOfInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = Guid.NewGuid(),
                Step = step,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = recipeGuid,
            };
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction });
            await Assert.ThrowsAsync<InstructionAlreadyExisteException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateListOfInstructionCommand : Ok")]
        public async void CreateListOfInstructionCommandTest_OK()
        {
            Guid? guid = Guid.NewGuid();
            int step = 0;
            string stepName = "stepName";
            string stepDescription = "StepDescription";
            CreateListOfInstructionCommand query = new CreateListOfInstructionCommand(new List<CreateListOfInstructionCommand.Instruction>()
            {
                new CreateListOfInstructionCommand.Instruction(Guid.NewGuid(), 0, stepName, stepDescription)
            });
            CreateListOfInstructionCommandHandler handler = new CreateListOfInstructionCommandHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new Domain.Entity.Instruction()
            {
                Id = Guid.NewGuid(),
                Step = step,
                StepName = stepName,
                StepInstruction = stepDescription,
                RecipeId = Guid.NewGuid(),
            };
            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction });
            await Assert.ThrowsAsync<InstructionAlreadyExisteException>(async () => await handler.Handle(query, _cancellationToken));
        }
    }
}
