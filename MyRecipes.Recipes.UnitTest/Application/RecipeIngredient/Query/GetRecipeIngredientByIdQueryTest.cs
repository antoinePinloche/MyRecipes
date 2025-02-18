using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Query
{
    public sealed class GetRecipeIngredientByIdQueryTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<ILogger<GetRecipeIngredientByIdQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetRecipeIngredientByIdQuery : WrongParameterException")]
        public async void GetRecipeIngredientByIdQueryTest_WrongParameterException()
        {
            Guid guid = Guid.Empty;
            GetRecipeIngredientByIdQuery query = new GetRecipeIngredientByIdQuery(guid);
            GetRecipeIngredientByIdQueryHandler handler = new GetRecipeIngredientByIdQueryHandler(_recipeIngredientRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeIngredientByIdQuery : RecipeIngredientNotFoundException")]
        public async void GetRecipeIngredientByIdQueryTest_RecipeIngredientNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            GetRecipeIngredientByIdQuery query = new GetRecipeIngredientByIdQuery(guid);
            GetRecipeIngredientByIdQueryHandler handler = new GetRecipeIngredientByIdQueryHandler(_recipeIngredientRepository.Object, _logger.Object);


            await Assert.ThrowsAsync<RecipeIngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeIngredientByIdQuery : Ok")]
        public async void GetRecipeIngredientByIdQueryTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            GetRecipeIngredientByIdQuery query = new GetRecipeIngredientByIdQuery(guid);
            GetRecipeIngredientByIdQueryHandler handler = new GetRecipeIngredientByIdQueryHandler(_recipeIngredientRepository.Object, _logger.Object);

            _recipeIngredientRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.RecipeIngredient()
                    {
                        Id = guid,
                        RecipeId = Guid.NewGuid(),
                        Ingredient = null,
                        IngredientId = Guid.NewGuid(),
                        Quantity = 1,
                        Unit = UnitOfMeasure.Kg
                    }
                )
                ;
            GetRecipeIngredientByIdQueryResult result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Equal(guid, result.Id);
        }
    }
}
