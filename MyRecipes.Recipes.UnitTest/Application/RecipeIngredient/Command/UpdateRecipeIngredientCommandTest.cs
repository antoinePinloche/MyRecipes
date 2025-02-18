using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.UpdateRecipeIngredient;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Command
{
    public sealed class UpdateRecipeIngredientCommandTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<IIngredientRepository> _ingredientRepository = new();
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<UpdateRecipeIngredientCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteRecipeIngredientCommand : WrongParameterException Id")]
        public async void DeleteRecipeIngredientCommand_WrongParameterException_Id()
        {
            Guid guid = Guid.Empty;
            Guid ingredientGuid = Guid.Empty;
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.Empty;

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, ingredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : WrongParameterException RecipeId")]
        public async void DeleteRecipeIngredientCommand_WrongParameterException_RecipeId()
        {
            Guid guid = Guid.NewGuid();
            Guid ingredientGuid = Guid.Empty;
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.Empty;

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, ingredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : WrongParameterException ingredientGuid")]
        public async void DeleteRecipeIngredientCommand_WrongParameterException_ingredientGuid()
        {
            Guid guid = Guid.NewGuid();
            Guid ingredientGuid = Guid.Empty;
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.NewGuid();

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, ingredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : RecipeIngredientNotFoundException")]
        public async void DeleteRecipeIngredientCommand_RecipeIngredientNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            Guid ingredientGuid = Guid.NewGuid();
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.NewGuid();

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, ingredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            await Assert.ThrowsAsync<RecipeIngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : RecipeNotFoundException")]
        public async void DeleteRecipeIngredientCommand_RecipeNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeIngredientGuid = Guid.NewGuid();
            Guid ingredientGuid = Guid.NewGuid();
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.NewGuid();

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, recipeIngredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            _recipeIngredientRepository.Setup(s => s.GetAsync(It.IsAny<Guid>())).ReturnsAsync(
                new Domain.Entity.RecipeIngredient()
                {
                    Id = ingredientGuid,
                    Ingredient = new Domain.Entity.Ingredient()
                    {
                        Id = ingredientGuid,
                        Name = "Name",
                        FoodType = null,
                        FoodTypeId = Guid.NewGuid()
                    },
                    IngredientId = ingredientGuid,
                    Quantity = 1,
                    RecipeId = Guid.NewGuid(),
                    Unit = unitOfMeasure
                });
            await Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : RecipeIngredientAlreadyExistException")]
        public async void DeleteRecipeIngredientCommand_RecipeIngredientAlreadyExistException()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeIngredientGuid = Guid.NewGuid();
            Guid ingredientGuid = Guid.NewGuid();
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.NewGuid();

            Domain.Entity.RecipeIngredient ri = new Domain.Entity.RecipeIngredient()
            {
                Id = ingredientGuid,
                Ingredient = new Domain.Entity.Ingredient()
                {
                    Id = ingredientGuid,
                    Name = "Name",
                    FoodType = null,
                    FoodTypeId = Guid.NewGuid()
                },
                IngredientId = ingredientGuid,
                Quantity = 1,
                RecipeId = Guid.NewGuid(),
                Unit = unitOfMeasure
            };

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, ingredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            _recipeIngredientRepository.Setup(s => s.GetAsync(It.IsAny<Guid>())).ReturnsAsync(ri);

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>())).ReturnsAsync(
                new Domain.Entity.Recipe()
                {
                    Id = (Guid)ri.RecipeId,
                    RecipyDifficulty = 0,
                    Ingredients = new List<Domain.Entity.RecipeIngredient>() { ri },
                    Instructions = null,
                    Name = "Recipe Name",
                    NbGuest = 0,
                    TimeToPrepareRecipe = 20,
                });

            _recipeIngredientRepository.Setup(s => s.GetAllRecipeIngredientByRecipeIdlAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new List<Domain.Entity.RecipeIngredient>()
                    {
                        new Domain.Entity.RecipeIngredient()
                        {
                            Id = Guid.NewGuid(),
                            Ingredient = new Domain.Entity.Ingredient()
                            {
                                Id = ingredientGuid,
                                Name = "Name2",
                                FoodType = null,
                                FoodTypeId = Guid.NewGuid()
                            },
                            IngredientId = ri.IngredientId,
                            Quantity = 1,
                            RecipeId = ri.RecipeId,
                            Unit = unitOfMeasure
                        }
                    }
                );
            await Assert.ThrowsAsync<RecipeIngredientAlreadyExistException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : Ok")]
        public async void DeleteRecipeIngredientCommand_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeIngredientGuid = Guid.NewGuid();
            Guid ingredientGuid = Guid.NewGuid();
            double quantity = 0;
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kg;
            Guid? recipeId = Guid.NewGuid();

            Domain.Entity.RecipeIngredient ri = new Domain.Entity.RecipeIngredient()
            {
                Id = ingredientGuid,
                Ingredient = new Domain.Entity.Ingredient()
                {
                    Id = ingredientGuid,
                    Name = "Name",
                    FoodType = null,
                    FoodTypeId = Guid.NewGuid()
                },
                IngredientId = ingredientGuid,
                Quantity = 1,
                RecipeId = Guid.NewGuid(),
                Unit = unitOfMeasure
            };

            UpdateRecipeIngredientCommand query = new UpdateRecipeIngredientCommand(guid, recipeIngredientGuid, quantity, unitOfMeasure, recipeId);

            UpdateRecipeIngredientCommandHandler handler = new UpdateRecipeIngredientCommandHandler(
                _recipeIngredientRepository.Object,
                _ingredientRepository.Object,
                _recipesRepository.Object,
                _logger.Object);

            _recipeIngredientRepository.Setup(s => s.GetAsync(It.IsAny<Guid>())).ReturnsAsync(ri);

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>())).ReturnsAsync(
                new Domain.Entity.Recipe()
                {
                    Id = (Guid)ri.RecipeId,
                    RecipyDifficulty = 0,
                    Ingredients = new List<Domain.Entity.RecipeIngredient>() { ri },
                    Instructions = null,
                    Name = "Recipe Name",
                    NbGuest = 0,
                    TimeToPrepareRecipe = 20,
                });

            _recipeIngredientRepository.Setup(s => s.GetAllRecipeIngredientByRecipeIdlAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new List<Domain.Entity.RecipeIngredient>()
                    {
                        new Domain.Entity.RecipeIngredient()
                        {
                            Id = Guid.NewGuid(),
                            Ingredient = new Domain.Entity.Ingredient()
                            {
                                Id = ingredientGuid,
                                Name = "Name2",
                                FoodType = null,
                                FoodTypeId = Guid.NewGuid()
                            },
                            IngredientId = ri.IngredientId,
                            Quantity = 1,
                            RecipeId = ri.RecipeId,
                            Unit = unitOfMeasure
                        }
                    }
                );
            await handler.Handle(query, _cancellationToken);
        }
    }
}
