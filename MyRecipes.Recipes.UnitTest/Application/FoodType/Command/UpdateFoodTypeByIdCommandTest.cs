using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Command.UpdateFoodTypeById;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.FoodType.Command
{
    public sealed class UpdateFoodTypeByIdCommandTest
    {
        private readonly Mock<IFoodTypeRepository> _foodTypeRepository = new Mock<IFoodTypeRepository>();
        private readonly Mock<ILogger<UpdateFoodTypeByIdCommandHandler>> _logger = new Mock<ILogger<UpdateFoodTypeByIdCommandHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("UpdateFoodTypeByIdCommand : Wrong parameter exception with empty Id")]
        public async Task UpdateFoodTypeByIdCommandTest_WrongParameterException_withEmpty_IdAsync()
        {
            UpdateFoodTypeByIdCommand command = new UpdateFoodTypeByIdCommand(Guid.Empty, "N");
            UpdateFoodTypeByIdCommandHandler handler = new UpdateFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("UpdateFoodTypeByIdCommand : Wrong parameter exception with empty Name")]
        public async Task UpdateFoodTypeByIdCommandTest_WrongParameterException_withEmpty_NameAsync()
        {
            UpdateFoodTypeByIdCommand command = new UpdateFoodTypeByIdCommand(Guid.NewGuid(), "");
            UpdateFoodTypeByIdCommandHandler handler = new UpdateFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("UpdateFoodTypeByIdCommand : FoodTypeNotFoundException")]
        public async Task DeleteFoodTypeByIdQueryTest_FoodTypeNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            UpdateFoodTypeByIdCommand command = new UpdateFoodTypeByIdCommand(Guid.NewGuid(), "Name");
            UpdateFoodTypeByIdCommandHandler handler = new UpdateFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodTypeReturn = new Domain.Entity.FoodType() { Name = "Name", Id = guid };

            await Assert.ThrowsAsync<FoodTypeNotFoundException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("UpdateFoodTypeByIdCommand : FoodTypeAlreadyExistException")]
        public async Task DeleteFoodTypeByIdQueryTest_FoodTypeAlreadyExistException_WithTrueReturn()
        {
            Guid guid = Guid.NewGuid();
            UpdateFoodTypeByIdCommand command = new UpdateFoodTypeByIdCommand(Guid.NewGuid(), "Name");
            UpdateFoodTypeByIdCommandHandler handler = new UpdateFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodTypeReturn = new Domain.Entity.FoodType() { Name = "Name", Id = guid };
            _foodTypeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(foodTypeReturn);
            _foodTypeRepository.Setup(x => x.FoodTypeByName(It.IsAny<string>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<FoodTypeAlreadyExistException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("UpdateFoodTypeByIdCommand : Ok")]
        public async Task DeleteFoodTypeByIdQueryTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            UpdateFoodTypeByIdCommand command = new UpdateFoodTypeByIdCommand(Guid.NewGuid(), "Name");
            UpdateFoodTypeByIdCommandHandler handler = new UpdateFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodTypeReturn = new Domain.Entity.FoodType() { Name = "Name", Id = guid };
            _foodTypeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(foodTypeReturn);
            _foodTypeRepository.Setup(x => x.FoodTypeByName(It.IsAny<string>())).ReturnsAsync(false);

            await handler.Handle(command, _cancellationToken);
        }
    }
}
