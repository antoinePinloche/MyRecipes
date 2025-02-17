using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetAllRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Command
{
    public sealed class CreateRecipeIngredientCommandTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<ILogger<CreateRecipeIngredientCommandHandler>> _logger = new();
        private readonly Mock<ISender> _sender = new(); 
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CreateRecipeIngredientCommand : WrongParameterException ingredientId")]
        public async void CreateRecipeIngredientCommandTest_WrongParameterException_ingredientId()
        {
            Guid guid = Guid.Empty;
            double Quantity = 1;
            UnitOfMeasure Unit = UnitOfMeasure.L;
            Guid? recipeGuid = Guid.Empty;
            CreateRecipeIngredientCommand query = new CreateRecipeIngredientCommand()
            {
                IngredientId = guid,
                Quantity = Quantity,
                Unit = Unit,
                RecipeId = recipeGuid
            };

            CreateRecipeIngredientCommandHandler handler = new CreateRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateRecipeIngredientCommand : WrongParameterException RecipeId")]
        public async void CreateRecipeIngredientCommandTest_WrongParameterException_RecipeId()
        {
            Guid guid = Guid.NewGuid();
            double Quantity = 1;
            UnitOfMeasure Unit = UnitOfMeasure.L;
            Guid? recipeGuid = Guid.Empty;
            CreateRecipeIngredientCommand query = new CreateRecipeIngredientCommand()
            {
                IngredientId = guid,
                Quantity = Quantity,
                Unit = Unit,
                RecipeId = recipeGuid
            };

            CreateRecipeIngredientCommandHandler handler = new CreateRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateRecipeIngredientCommand : IngredientNotFoundException RecipeId")]
        public async void CreateRecipeIngredientCommandTest_IngredientNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            double Quantity = 1;
            UnitOfMeasure Unit = UnitOfMeasure.L;
            Guid? recipeGuid = Guid.NewGuid();
            CreateRecipeIngredientCommand query = new CreateRecipeIngredientCommand()
            {
                IngredientId = guid,
                Quantity = Quantity,
                Unit = Unit,
                RecipeId = recipeGuid
            };

            CreateRecipeIngredientCommandHandler handler = new CreateRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            await Assert.ThrowsAsync<IngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateRecipeIngredientCommand : RecipeIngredientAlreadyExistException RecipeId")]
        public async void CreateRecipeIngredientCommandTest_RecipeIngredientAlreadyExistException()
        {
            Guid guid = Guid.NewGuid();
            double Quantity = 1;
            UnitOfMeasure Unit = UnitOfMeasure.L;
            Guid? recipeGuid = Guid.NewGuid();

            CreateRecipeIngredientCommandHandler handler = new CreateRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            CreateRecipeIngredientCommand query = new CreateRecipeIngredientCommand()
            {
                IngredientId = guid,
                Quantity = Quantity,
                Unit = Unit,
                RecipeId = recipeGuid
            };

            _sender.Setup(s => s.Send(It.IsAny<GetIngredientByIdQuery>(), _cancellationToken)).ReturnsAsync(new GetIngredientByIdQueryResult()
            {
                Id = Guid.NewGuid(),
                FoodTypeName = "Fruit",
                Name = "Banane"
            });

            Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient()
            {
                Id = Guid.NewGuid(),
                FoodType = null,
                FoodTypeId = Guid.NewGuid(),
                Name = "Banane"
            };


            _sender.Setup(s => s.Send(It.IsAny<GetRecipeIngredientByRecipeIdQuery>(), _cancellationToken))
                .ReturnsAsync(new List<GetRecipeIngredientByRecipeIdQueryResult>()
                {
                    new GetRecipeIngredientByRecipeIdQueryResult(Guid.NewGuid(), ingredient.Id, ingredient, 1, UnitOfMeasure.Kg)
                });

            await Assert.ThrowsAsync<RecipeIngredientAlreadyExistException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateRecipeIngredientCommand : Ok")]
        public async void CreateRecipeIngredientCommandTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            double Quantity = 1;
            UnitOfMeasure Unit = UnitOfMeasure.L;
            Guid? recipeGuid = Guid.NewGuid();

            CreateRecipeIngredientCommandHandler handler = new CreateRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            CreateRecipeIngredientCommand query = new CreateRecipeIngredientCommand()
            {
                IngredientId = guid,
                Quantity = Quantity,
                Unit = Unit,
                RecipeId = recipeGuid
            };

            _sender.Setup(s => s.Send(It.IsAny<GetIngredientByIdQuery>(), _cancellationToken)).ReturnsAsync(new GetIngredientByIdQueryResult()
            {
                Id = Guid.NewGuid(),
                FoodTypeName = "Fruit",
                Name = "Banane"
            });

            Domain.Entity.Ingredient ingredient = new Domain.Entity.Ingredient()
            {
                Id = Guid.NewGuid(),
                FoodType = null,
                FoodTypeId = Guid.NewGuid(),
                Name = "Pomme"
            };


            _sender.Setup(s => s.Send(It.IsAny<GetRecipeIngredientByRecipeIdQuery>(), _cancellationToken))
                .ReturnsAsync(new List<GetRecipeIngredientByRecipeIdQueryResult>()
                {
                    new GetRecipeIngredientByRecipeIdQueryResult(Guid.NewGuid(), ingredient.Id, ingredient, 1, UnitOfMeasure.Kg)
                });

            await handler.Handle(query, _cancellationToken);
        }
    }
}
