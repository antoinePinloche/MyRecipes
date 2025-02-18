using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Query.CheckInstructionAcces;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Query
{
    public sealed class CheckInstructionAccesQueryTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<CheckInstructionAccesQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CheckInstructionAccesQuery : WrongParameterException recipeID")]
        public async Task CheckInstructionAccesQueryTest_WrongParameterException_recipeID()
        {
            Guid recipeID = Guid.Empty;
            Guid userId = Guid.Empty;

            CheckInstructionAccesQuery query = new CheckInstructionAccesQuery(recipeID, userId);
            CheckInstructionAccesQueryHandler handler = new CheckInstructionAccesQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CheckInstructionAccesQuery : WrongParameterException userId")]
        public async Task CheckInstructionAccesQueryTest_WrongParameterException_userID()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.Empty;

            CheckInstructionAccesQuery query = new CheckInstructionAccesQuery(recipeID, userId);
            CheckInstructionAccesQueryHandler handler = new CheckInstructionAccesQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }
        [Fact]
        [Description("CheckInstructionAccesQuery : InstructionNotFoundException")]
        public async Task CheckInstructionAccesQueryTest_InstructionNotFoundException()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckInstructionAccesQuery query = new CheckInstructionAccesQuery(recipeID, userId);
            CheckInstructionAccesQueryHandler handler = new CheckInstructionAccesQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }


        [Fact]
        [Description("CheckInstructionAccesQuery : Ok false return")]
        public async Task CheckInstructionAccesQueryTest_WrongParameterException_Ok_falseReturn()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckInstructionAccesQuery query = new CheckInstructionAccesQuery(recipeID, userId);
            CheckInstructionAccesQueryHandler handler = new CheckInstructionAccesQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            _instructionRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.Instruction()
                    {
                        Id = recipeID,
                        Step = 0,
                        StepInstruction = "",
                        StepName = "stepName",
                        RecipeId = Guid.NewGuid(),
                    });

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                new Domain.Entity.Recipe()
                {
                    Id = recipeID,
                    RecipyDifficulty = 0,
                    Name = "Name",
                    NbGuest = 1,
                    TimeToPrepareRecipe = 1,
                    UserId = Guid.NewGuid(),
                });
            var result = await handler.Handle(query, _cancellationToken);
            Assert.False(result);
        }

        [Fact]
        [Description("CheckInstructionAccesQuery : Ok true return")]
        public async Task CheckInstructionAccesQueryTest_WrongParameterException_Ok_trueReturn()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckInstructionAccesQuery query = new CheckInstructionAccesQuery(recipeID, userId);
            CheckInstructionAccesQueryHandler handler = new CheckInstructionAccesQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            _instructionRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.Instruction()
                    {
                        Id = recipeID,
                        Step = 0,
                        StepInstruction = "",
                        StepName = "stepName",
                        RecipeId = Guid.NewGuid(),
                    });

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                new Domain.Entity.Recipe()
                {
                    Id = recipeID,
                    RecipyDifficulty = 0,
                    Name = "Name",
                    NbGuest = 1,
                    TimeToPrepareRecipe = 1,
                    UserId = userId
                });
            var result = await handler.Handle(query, _cancellationToken);
            Assert.False(result);
        }
    }
}
