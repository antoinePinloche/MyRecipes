using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Query
{
    public sealed class GetAllRecipeQueryTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<GetAllRecipeQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetAllRecipeQuery : Ok ")]
        public async void GetAllRecipeQueryTest_Ok()
        {
            GetAllRecipeQuery query = new GetAllRecipeQuery();
            GetAllRecipeQueryHandler handler = new GetAllRecipeQueryHandler(_recipesRepository.Object, _logger.Object);
            _recipesRepository.Setup(s => s.GetAllAsync())
              .ReturnsAsync(
                new List<Domain.Entity.Recipe>()
                {
                  new Domain.Entity.Recipe()
                  {
                      Id = Guid.NewGuid(),
                      Name = "Name",
                      RecipyDifficulty = Difficulty.Normal,
                      TimeToPrepareRecipe = 80,
                      NbGuest = 2,
                      Ingredients = null,
                      Instructions = null
                  }
                }
              );
            List<GetAllRecipeQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}
