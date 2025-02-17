using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Query
{
    public sealed class GetRecipeByIdQueryTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<GetRecipeByIdQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetRecipeByIdQuery : WrongParameterException ")]
        public void GetRecipeByIdQueryTest_WrongParameterException_id()
        {
            Guid guid = Guid.Empty;

            string name = null;
            Domain.Entity.Enum.Difficulty difficulty = Domain.Entity.Enum.Difficulty.Normal;
            int nbGuest = 1;
            int TimeToPrepareRecipe = 120;

            GetRecipeByIdQuery query = new GetRecipeByIdQuery(guid);
            GetRecipeByIdQueryHandler handler = new GetRecipeByIdQueryHandler(_recipesRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeByIdQuery : RecipeNotFoundException ")]
        public void GetRecipeByIdQueryTest_RecipeNotFoundException()
        {
            Guid guid = Guid.NewGuid();

            GetRecipeByIdQuery query = new GetRecipeByIdQuery(guid);
            GetRecipeByIdQueryHandler handler = new GetRecipeByIdQueryHandler(_recipesRepository.Object, _logger.Object);

            Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeByIdQuery : Ok ")]
        public async void GetRecipeByIdQueryTest_Ok()
        {
            Guid guid = Guid.NewGuid();

            GetRecipeByIdQuery query = new GetRecipeByIdQuery(guid);
            GetRecipeByIdQueryHandler handler = new GetRecipeByIdQueryHandler(_recipesRepository.Object, _logger.Object);
            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.Recipe()
                    {
                        Id = Guid.NewGuid(),
                        Name = "new Name",
                        RecipyDifficulty = Difficulty.Normal,
                        TimeToPrepareRecipe = 80,
                        NbGuest = 2,
                        Ingredients = null,
                        Instructions = null
                    }
                );
            await handler.Handle(query, _cancellationToken);
        }
        
    }
}
