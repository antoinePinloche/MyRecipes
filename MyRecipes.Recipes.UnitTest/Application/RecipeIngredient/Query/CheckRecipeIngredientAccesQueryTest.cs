using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.CheckRecipeIngredientAcces;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Query
{
    public class CheckRecipeIngredientAccesQueryTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<CheckRecipeIngredientAccesQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CheckRecipeIngredientAccesQuery : WrongParameterException recipeID")]
        public async Task CheckRecipeIngredientAccesQueryTest_WrongParameterException_recipeID()
        {
            Guid recipeID = Guid.Empty;
            Guid userId = Guid.Empty;

            CheckRecipeIngredientAccesQuery query = new CheckRecipeIngredientAccesQuery(recipeID, userId);
            CheckRecipeIngredientAccesQueryHandler handler = new CheckRecipeIngredientAccesQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CheckRecipeIngredientAccesQuery : WrongParameterException userId")]
        public async Task CheckRecipeIngredientAccesQueryTest_WrongParameterException_userID()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.Empty;

            CheckRecipeIngredientAccesQuery query = new CheckRecipeIngredientAccesQuery(recipeID, userId);
            CheckRecipeIngredientAccesQueryHandler handler = new CheckRecipeIngredientAccesQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }
        [Fact]
        [Description("CheckRecipeIngredientAccesQuery : InstructionNotFoundException")]
        public async Task CheckRecipeIngredientAccesQueryTest_InstructionNotFoundException()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckRecipeIngredientAccesQuery query = new CheckRecipeIngredientAccesQuery(recipeID, userId);
            CheckRecipeIngredientAccesQueryHandler handler = new CheckRecipeIngredientAccesQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }
        

        [Fact]
        [Description("CheckRecipeIngredientAccesQuery : Ok false return")]
        public async Task CheckRecipeIngredientAccesQueryTest_WrongParameterException_Ok_falseReturn()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckRecipeIngredientAccesQuery query = new CheckRecipeIngredientAccesQuery(recipeID, userId);
            CheckRecipeIngredientAccesQueryHandler handler = new CheckRecipeIngredientAccesQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            _recipeIngredientRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.RecipeIngredient()
                    {
                        Id = recipeID,
                        IngredientId = Guid.NewGuid(),
                        Quantity = 1,
                        RecipeId = Guid.NewGuid(),
                        Unit = 0
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
        [Description("CheckRecipeIngredientAccesQuery : Ok true return")]
        public async Task CheckRecipeIngredientAccesQueryTest_WrongParameterException_Ok_trueReturn()
        {
            Guid recipeID = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            CheckRecipeIngredientAccesQuery query = new CheckRecipeIngredientAccesQuery(recipeID, userId);
            CheckRecipeIngredientAccesQueryHandler handler = new CheckRecipeIngredientAccesQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            _recipeIngredientRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.RecipeIngredient()
                    {
                        Id = recipeID,
                        IngredientId = Guid.NewGuid(),
                        Quantity = 1,
                        RecipeId = recipeID,
                        Unit = 0
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
