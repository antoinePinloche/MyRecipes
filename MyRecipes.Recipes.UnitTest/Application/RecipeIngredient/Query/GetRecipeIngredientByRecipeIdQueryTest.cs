using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Query
{
    public sealed class GetRecipeIngredientByRecipeIdQueryTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<GetRecipeIngredientByRecipeIdQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetRecipeIngredientByRecipeIdQuery : WrongParameterException")]
        public async void GetRecipeIngredientByRecipeIdQueryTest_WrongParameterException()
        {
            Guid guid = Guid.Empty;
            GetRecipeIngredientByRecipeIdQuery query = new GetRecipeIngredientByRecipeIdQuery(guid);
            GetRecipeIngredientByRecipeIdQueryHandler handler = new GetRecipeIngredientByRecipeIdQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeIngredientByRecipeIdQuery : RecipeNotFoundException")]
        public async void GetRecipeIngredientByRecipeIdQueryTest_RecipeNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            GetRecipeIngredientByRecipeIdQuery query = new GetRecipeIngredientByRecipeIdQuery(guid);
            GetRecipeIngredientByRecipeIdQueryHandler handler = new GetRecipeIngredientByRecipeIdQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeIngredientByRecipeIdQuery : RecipeNotFoundException")]
        public async void GetRecipeIngredientByRecipeIdQueryTest_RecipeIngredientNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            GetRecipeIngredientByRecipeIdQuery query = new GetRecipeIngredientByRecipeIdQuery(guid);
            GetRecipeIngredientByRecipeIdQueryHandler handler = new GetRecipeIngredientByRecipeIdQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);

            Domain.Entity.RecipeIngredient ri = new Domain.Entity.RecipeIngredient()
            {
                Id = guid,
                RecipeId = Guid.NewGuid(),
                Ingredient = null,
                IngredientId = Guid.NewGuid(),
                Quantity = 1,
                Unit = UnitOfMeasure.Kg
            };
            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.Recipe()
                    {
                        Id = guid,
                        RecipyDifficulty = 0,
                        Instructions = null,
                        Ingredients = new List<Domain.Entity.RecipeIngredient>() { ri },
                        Name = "Banane flambée",
                        NbGuest = 0,
                        TimeToPrepareRecipe = 120
                    }
                );
            await Assert.ThrowsAsync<RecipeIngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetRecipeIngredientByRecipeIdQuery : Ok")]
        public async void GetRecipeIngredientByRecipeIdQueryTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            GetRecipeIngredientByRecipeIdQuery query = new GetRecipeIngredientByRecipeIdQuery(guid);
            GetRecipeIngredientByRecipeIdQueryHandler handler = new GetRecipeIngredientByRecipeIdQueryHandler(_recipeIngredientRepository.Object, _recipesRepository.Object, _logger.Object);
            Domain.Entity.RecipeIngredient ri = new Domain.Entity.RecipeIngredient()
            {
                Id = guid,
                RecipeId = Guid.NewGuid(),
                Ingredient = null,
                IngredientId = Guid.NewGuid(),
                Quantity = 1,
                Unit = UnitOfMeasure.Kg
            };

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Domain.Entity.Recipe()
                    {
                        Id = guid,
                        RecipyDifficulty = 0,
                        Instructions = null,
                        Ingredients = new List<Domain.Entity.RecipeIngredient>() { ri },
                        Name = "Banane flambée",
                        NbGuest = 0,
                        TimeToPrepareRecipe = 120
                    }
                );
            _recipeIngredientRepository.Setup(s => s.GetAllRecipeIngredientByRecipeIdlAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Domain.Entity.RecipeIngredient>() { ri });
            List<GetRecipeIngredientByRecipeIdQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(guid, result[0].Id);
        }
    }
}
