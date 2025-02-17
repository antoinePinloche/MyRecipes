using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Command.CreateFoodType;
using MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.FoodType.Query
{
    public sealed class GetFoodTypeByIdQueryTest
    {
        private readonly Mock<IFoodTypeRepository> _foodTypeRepository = new Mock<IFoodTypeRepository>();
        private readonly Mock<ILogger<GetFoodTypeByIdQueryHandler>> _logger = new Mock<ILogger<GetFoodTypeByIdQueryHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetFoodTypeByIdQuery : Wrong parameter exception")]
        public void GetFoodTypeByIdQueryTest_WrongParameterException_withEmpty_Name()
        {
            GetFoodTypeByIdQuery query = new GetFoodTypeByIdQuery(Guid.Empty);
            GetFoodTypeByIdQueryHandler handler = new GetFoodTypeByIdQueryHandler(_foodTypeRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetFoodTypeByIdQuery : empty return")]
        public async void GetFoodTypeByIdQueryTest_Ok_WithEmptyReturn()
        {
            Guid guid = Guid.NewGuid();
            GetFoodTypeByIdQuery query = new GetFoodTypeByIdQuery(guid);
            GetFoodTypeByIdQueryHandler handler = new GetFoodTypeByIdQueryHandler(_foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodTypeReturn = null;
            _foodTypeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(foodTypeReturn);
            GetFoodTypeByIdQueryResult result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Null(result.FoodType);
        }

        [Fact]
        [Description("GetFoodTypeByIdQuery : with return")]
        public async Task GetFoodTypeByIdQueryTest_Ok_WithReturnAsync()
        {
            Guid guid = Guid.NewGuid();
            GetFoodTypeByIdQuery query = new GetFoodTypeByIdQuery(guid);
            GetFoodTypeByIdQueryHandler handler = new GetFoodTypeByIdQueryHandler(_foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodTypeReturn = new Domain.Entity.FoodType() { Name = "Name", Id = guid };
            _foodTypeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(foodTypeReturn);
            GetFoodTypeByIdQueryResult result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Equal(foodTypeReturn.Name, result.FoodType.Name);
            Assert.Equal(foodTypeReturn.Id, result.FoodType.Id);
        }
    }
}
