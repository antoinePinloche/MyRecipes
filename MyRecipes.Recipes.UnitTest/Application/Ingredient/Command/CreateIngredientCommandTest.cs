using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Application.Ingredient.Command.CreateIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.Ingredient.Command
{
    public sealed class CreateIngredientCommandTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<ILogger<CreateIngredientCommandHandler>> _logger = new Mock<ILogger<CreateIngredientCommandHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CreateIngredientCommand : Wrong parameter exception with empty Name")]
        public void CreateIngredientCommandest_WrongParameterException_withEmpty_Name()
        {
            CreateIngredientCommand command = new CreateIngredientCommand("", Guid.Empty);
            CreateIngredientCommandHandler handler = new CreateIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("CreateIngredientCommand : Wrong parameter exception with empty Id")]
        public void CreateIngredientCommandest_WrongParameterException_withEmpty_Id()
        {
            CreateIngredientCommand command = new CreateIngredientCommand("Name", Guid.Empty);
            CreateIngredientCommandHandler handler = new CreateIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("CreateIngredientCommand : Wrong parameter exception with empty Id")]
        public void CreateIngredientCommandest_WrongParameterException_IngredientAlreadyExistException()
        {
            Guid guid = Guid.NewGuid();
            Guid foodTypeId = Guid.NewGuid();
            CreateIngredientCommand command = new CreateIngredientCommand("Name", guid);
            CreateIngredientCommandHandler handler = new CreateIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);
            Domain.Entity.Ingredient ingredientReturn = new Domain.Entity.Ingredient()
            {
                Id = guid,
                FoodType = new Domain.Entity.FoodType()
                {
                    Id = foodTypeId,
                    Name = "Fruit"
                },
                FoodTypeId = foodTypeId,
                Name = "Already"
            };
            _ingredientRepository.Setup(x => x.HasIngredient(It.IsAny<string>())).ReturnsAsync(ingredientReturn);

            Assert.ThrowsAsync<IngredientAlreadyExistException>(async () => await handler.Handle(command, _cancellationToken));
        }


        [Fact]
        [Description("CreateIngredientCommand : Ok")]
        public async void CreateIngredientCommandest_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid foodTypeId = Guid.NewGuid();
            CreateIngredientCommand command = new CreateIngredientCommand("Name", guid);
            CreateIngredientCommandHandler handler = new CreateIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);
            Domain.Entity.Ingredient ingredientReturn = new Domain.Entity.Ingredient()
            {
                Id = guid,
                FoodType = new Domain.Entity.FoodType()
                {
                    Id = foodTypeId,
                    Name = "Fruit"
                },
                FoodTypeId = foodTypeId,
                Name = "Already"
            };
            _ingredientRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entity.Ingredient>())).ReturnsAsync(ingredientReturn);
            
            await handler.Handle(command, _cancellationToken);
        }
    }
}
