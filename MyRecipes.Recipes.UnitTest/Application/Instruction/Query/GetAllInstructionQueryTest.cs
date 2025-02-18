using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction;
using MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Query
{
    public sealed class GetAllInstructionQueryTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<ILogger<GetAllInstructionQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetAllInstructionQuery : Ok")]
        public async void GetAllInstructionQuery_Ok()
        {
            GetAllInstructionQuery query = new GetAllInstructionQuery();
            GetAllInstructionQueryHandler handler = new GetAllInstructionQueryHandler(_instructionRepository.Object, _logger.Object);


            Guid recipeGuid = Guid.NewGuid();

            Domain.Entity.Instruction instruction = new() 
            {
                Id = Guid.NewGuid(),
                RecipeId = recipeGuid,
                Step = 0,
                StepInstruction = "couper la petite tranche les bananes",
                StepName = "préparation"
            };
            Domain.Entity.Instruction instruction2 = new()
            {
                Id = Guid.NewGuid(),
                RecipeId = recipeGuid,
                Step = 1,
                StepInstruction = "faire chauffer la poele",
                StepName = "préparation"
            };

            _instructionRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(
                new List<Domain.Entity.Instruction>()
                {
                    instruction, instruction2
                });
            List<GetAllInstructionQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(instruction.Id, result[0].Id);
            Assert.Equal(instruction.Step, result[0].Step);
            Assert.Equal(instruction.StepName, result[0].StepName);
            Assert.Equal(instruction.StepInstruction, result[0].StepInstruction);
            Assert.Equal(instruction2.Id, result[1].Id);
            Assert.Equal(instruction2.Step, result[1].Step);
            Assert.Equal(instruction2.StepName, result[1].StepName);
            Assert.Equal(instruction2.StepInstruction, result[1].StepInstruction);

        }
    }
}
