﻿using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Query
{
    public sealed class GetRecipeByNameQueryTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<GetRecipeByNameQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetRecipeByNameQuery : WrongParameterException ")]
        public async Task GetRecipeByNameQueryTest_WrongParameterException_idAsync()
        {
            string name = string.Empty;

            GetRecipeByNameQuery query = new GetRecipeByNameQuery(name);
            GetRecipeByNameQueryHandler handler = new GetRecipeByNameQueryHandler(_recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeByNameQuery : RecipeNotFoundException ")]
        public async Task GetRecipeByNameQueryTest_RecipeNotFoundExceptionAsync()
        {
            string name = "Banane flambée";

            GetRecipeByNameQuery query = new GetRecipeByNameQuery(name);
            GetRecipeByNameQueryHandler handler = new GetRecipeByNameQueryHandler(_recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeByNameQuery : Ok ")]
        public async void GetRecipeByNameQueryTest_Ok()
        {
            string name = "Banane flambée";

            GetRecipeByNameQuery query = new GetRecipeByNameQuery(name);
            GetRecipeByNameQueryHandler handler = new GetRecipeByNameQueryHandler(_recipesRepository.Object, _logger.Object);
            _recipesRepository.Setup(s => s.GetByNameAsync(It.IsAny<string>()))
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
            List<GetRecipeByNameQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}
