using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.FoodType.Query
{
    public class GetAllFoodTypeQueryTest
    {
        private readonly Mock<IFoodTypeRepository> _foodTypeRepository = new Mock<IFoodTypeRepository>();
        private readonly Mock<ILogger<GetAllFoodTypeQueryHandler>> _logger = new Mock<ILogger<GetAllFoodTypeQueryHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetAllFoodTypeQuery : Ok empty return")]
        public async Task GetAllFoodTypeQueryTest_Ok_withEmptyListAsync()
        {
            GetAllFoodTypeQuery command = new GetAllFoodTypeQuery();
            GetAllFoodTypeQueryHandler handler = new GetAllFoodTypeQueryHandler(_foodTypeRepository.Object, _logger.Object);
            List<Domain.Entity.FoodType> listFound = new List<Domain.Entity.FoodType>();

            _foodTypeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listFound);
            GetAllFoodTypeQueryResult result = await handler.Handle(command, _cancellationToken);
            Assert.NotNull(result);
            Assert.Empty(result.FoodType);
        }

        [Fact]
        [Description("GetAllFoodTypeQuery : Ok return")]
        public async void GetAllFoodTypeQueryTest_Ok_withList()
        {
            GetAllFoodTypeQuery command = new GetAllFoodTypeQuery();
            GetAllFoodTypeQueryHandler handler = new GetAllFoodTypeQueryHandler(_foodTypeRepository.Object, _logger.Object);
            List<Domain.Entity.FoodType> listFound = new List<Domain.Entity.FoodType>()
            {
                new Domain.Entity.FoodType(){Name = "Name", Id = Guid.NewGuid() },
                new Domain.Entity.FoodType(){Name = "Name2", Id = Guid.NewGuid() }
            };

            _foodTypeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listFound);
            GetAllFoodTypeQueryResult result = await handler.Handle(command, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result.FoodType);
            Assert.Equal(2, result.FoodType.Count);
            Assert.Equal("Name", result.FoodType[0].Name);
            Assert.Equal("Name2", result.FoodType[1].Name);
        }
    }
}
