﻿using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.FoodType.Command
{
    public sealed class CreateFoodTypeCommandTest
    {
        private readonly Mock<IFoodTypeRepository> _foodTypeRepository = new Mock<IFoodTypeRepository>();
        private readonly Mock<ILogger<CreateFoodTypeCommandHandler>> _logger = new Mock<ILogger<CreateFoodTypeCommandHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;
        
        [Fact]
        [Description("CreateFoodTypeQuery : Wrong parameter exception with empty Name")]
        public async Task CreateFoodTypeQueryTest_WrongParameterException_withEmpty_NameAsync()
        {
            CreateFoodTypeCommand command = new CreateFoodTypeCommand("");
            CreateFoodTypeCommandHandler handler = new CreateFoodTypeCommandHandler(_foodTypeRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("CreateFoodTypeQuery : Wrong parameter exception")]
        public async void CreateFoodTypeQueryTest_WrongParameterException_null_Name()
        {
            CreateFoodTypeCommand command = new CreateFoodTypeCommand(null);
            CreateFoodTypeCommandHandler handler = new CreateFoodTypeCommandHandler(_foodTypeRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("CreateFoodTypeQuery : FoodTypeAlreadyExistException")]
        public async Task CreateFoodTypeQueryTest_FoodTypeAlreadyExistException()
        {
            CreateFoodTypeCommand command = new CreateFoodTypeCommand("Name");
            CreateFoodTypeCommandHandler handler = new CreateFoodTypeCommandHandler(_foodTypeRepository.Object, _logger.Object);

            _foodTypeRepository.Setup(x => x.FoodTypeExist(It.IsAny<string>())).ReturnsAsync(false);

            await Assert.ThrowsAsync<FoodTypeAlreadyExistException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("CreateFoodTypeQuery : Ok")]
        public async Task CreateFoodTypeQueryTest_OkAsync()
        {
            CreateFoodTypeCommand command = new CreateFoodTypeCommand("Name");
            CreateFoodTypeCommandHandler handler = new CreateFoodTypeCommandHandler(_foodTypeRepository.Object, _logger.Object);

            _foodTypeRepository.Setup(x => x.FoodTypeExist(It.IsAny<string>())).ReturnsAsync(true);
            Domain.Entity.FoodType foodTypeReturn = new Domain.Entity.FoodType() {Name = "Name", Id = Guid.NewGuid()};
            _foodTypeRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entity.FoodType>())).ReturnsAsync(foodTypeReturn);

            await handler.Handle(command, _cancellationToken);
        }
    }
}
