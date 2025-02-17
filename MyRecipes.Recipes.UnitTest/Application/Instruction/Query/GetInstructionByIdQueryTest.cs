using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Query
{
    public sealed class GetInstructionByIdQueryTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<GetInstructionByIdQueryHandler>> _logger = new ();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetInstructionByIdQuery : WrongParameterException")]
        public void GetInstructionByIdQueryTest_WrongParameterException()
        {
            Guid guid = Guid.Empty;
            GetInstructionByIdQuery query = new GetInstructionByIdQuery(guid);
            GetInstructionByIdQueryHandler handler = new GetInstructionByIdQueryHandler(_instructionRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetInstructionByIdQuery : InstructionNotFoundException")]
        public void GetInstructionByIdQueryTest_InstructionNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeGuid = Guid.NewGuid();

            GetInstructionByIdQuery query = new GetInstructionByIdQuery(guid);
            GetInstructionByIdQueryHandler handler = new GetInstructionByIdQueryHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 0, StepInstruction = "", StepName = "préparation" };
            
            _instructionRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()));
            Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetInstructionByIdQuery : Ok")]
        public async void GetInstructionByIdQueryTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeGuid = Guid.NewGuid();

            GetInstructionByIdQuery query = new GetInstructionByIdQuery(guid);
            GetInstructionByIdQueryHandler handler = new GetInstructionByIdQueryHandler(_instructionRepository.Object, _logger.Object);

            Domain.Entity.Instruction instruction = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 0, StepInstruction = "", StepName = "préparation" };

            _instructionRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(instruction);
            GetInstructionByIdQueryResult result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Equal(instruction.Id, result.Id);
            Assert.Equal(instruction.Step, result.Step);
            Assert.Equal(instruction.StepName, result.StepName);
            Assert.Equal(instruction.StepInstruction, result.StepInstruction);

        }
    }
}
