using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Query.CheckRecipeAcces;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Query
{
    public sealed class CheckRecipeAccesQueryTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<CheckRecipeAccesQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CheckRecipeAccesQuery : WrongParameterException recipeID")]
        public async Task GCheckRecipeAccesQueryTest_WrongParameterException_recipeID()
        {
            Guid recipeID = Guid.Empty;
            Guid userId = Guid.Empty;

            CheckRecipeAccesQuery query = new CheckRecipeAccesQuery(recipeID, userId);
            CheckRecipeAccesQueryHandler handler = new CheckRecipeAccesQueryHandler(_recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CheckRecipeAccesQuery : WrongParameterException userId")]
        public async Task CheckRecipeAccesQueryTest_WrongParameterException_userID()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.Empty;

            CheckRecipeAccesQuery query = new CheckRecipeAccesQuery(recipeID, userId);
            CheckRecipeAccesQueryHandler handler = new CheckRecipeAccesQueryHandler(_recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CheckRecipeAccesQuery : Ok false return")]
        public async Task CheckRecipeAccesQueryTest_WrongParameterException_Ok_falseReturn()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckRecipeAccesQuery query = new CheckRecipeAccesQuery(recipeID, userId);
            CheckRecipeAccesQueryHandler handler = new CheckRecipeAccesQueryHandler(_recipesRepository.Object, _logger.Object);

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
        [Description("CheckRecipeAccesQuery : Ok true return")]
        public async Task CheckRecipeAccesQueryTest_WrongParameterException_Ok_trueReturn()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckRecipeAccesQuery query = new CheckRecipeAccesQuery(recipeID, userId);
            CheckRecipeAccesQueryHandler handler = new CheckRecipeAccesQueryHandler(_recipesRepository.Object, _logger.Object);

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
