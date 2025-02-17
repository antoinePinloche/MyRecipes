using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Query.GetAllRecipe;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Query
{
    public sealed class GetAllRecipeIngredientQueryTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<ILogger<GetAllRecipeIngredientQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetAllRecipeIngredientQuery : Ok ")]
        public async void GetAllRecipeIngredientQueryTest_Ok()
        {
            GetAllRecipeIngredientQuery query = new GetAllRecipeIngredientQuery();
            GetAllRecipeIngredientQueryHandler handler = new GetAllRecipeIngredientQueryHandler(_recipeIngredientRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodType = new Domain.Entity.FoodType()
            {
                Id = Guid.NewGuid(),
                Name = "Fruit"
            };

            Domain.Entity.Ingredient ingredient = new()
            {
                Id = Guid.NewGuid(),
                FoodType = foodType,
                FoodTypeId = foodType.Id,
                Name = "Banane"
            };

            _recipeIngredientRepository.Setup(s => s.GetAllAsync())
              .ReturnsAsync(
                new List<Domain.Entity.RecipeIngredient>()
                {
                  new Domain.Entity.RecipeIngredient()
                  {
                      Id = Guid.NewGuid(),
                      Quantity = 1,
                      Unit = UnitOfMeasure.Kg,
                      Ingredient = ingredient,
                      IngredientId = ingredient.Id,
                      RecipeId = Guid.NewGuid()
                  }
                }
              );
            List<GetAllRecipeIngredientQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}
