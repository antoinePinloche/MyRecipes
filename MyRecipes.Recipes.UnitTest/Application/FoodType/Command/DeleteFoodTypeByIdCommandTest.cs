using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Command.DeleteFoodTypeById;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.FoodType.Command
{
    public sealed class DeleteFoodTypeByIdCommandTest
    {
        private readonly Mock<IFoodTypeRepository> _foodTypeRepository = new Mock<IFoodTypeRepository>();
        private readonly Mock<ILogger<DeleteFoodTypeByIdCommandHandler>> _logger = new Mock<ILogger<DeleteFoodTypeByIdCommandHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteFoodTypeByIdQuery : Wrong parameter exception with empty Id")]
        public async Task DeleteFoodTypeByIdQueryTest_WrongParameterException_withEmpty_NameAsync()
        {
            DeleteFoodTypeByIdCommand command = new DeleteFoodTypeByIdCommand(Guid.Empty);
            DeleteFoodTypeByIdCommandHandler handler = new DeleteFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("DeleteFoodTypeByIdQuery : FoodTypeNotFoundException")]
        public async Task DeleteFoodTypeByIdQueryTest_WrongParameterException_null_NameAsync()
        {
            DeleteFoodTypeByIdCommand command = new DeleteFoodTypeByIdCommand(Guid.NewGuid());
            DeleteFoodTypeByIdCommandHandler handler = new DeleteFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<FoodTypeNotFoundException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("DeleteFoodTypeByIdQuery : Ok")]
        public async Task DeleteFoodTypeByIdQueryTest_OkAsync()
        {
            Guid guid = Guid.NewGuid();
            DeleteFoodTypeByIdCommand command = new DeleteFoodTypeByIdCommand(guid);
            DeleteFoodTypeByIdCommandHandler handler = new DeleteFoodTypeByIdCommandHandler(_foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodTypeReturn = new Domain.Entity.FoodType() { Name = "Name", Id = guid };
            _foodTypeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(foodTypeReturn);

            await handler.Handle(command, _cancellationToken);
        }
    }
}
