using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.Ingredient.Query
{
    public class GetIngredientByIdQueryTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<ILogger<GetIngredientByIdQueryHandler>> _logger = new Mock<ILogger<GetIngredientByIdQueryHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetIngredientByIdQuery : WrongParameterException")]
        public void GetIngredientByIdQueryTest_WrongParameterException_withEmpty_Name()
        {
            GetIngredientByIdQuery query = new GetIngredientByIdQuery(Guid.Empty);
            GetIngredientByIdQueryHandler handler = new GetIngredientByIdQueryHandler(_ingredientRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetIngredientByIdQuery : IngredientNotFoundException")]
        public async void GetIngredientByIdQueryTest_IngredientNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            GetIngredientByIdQuery query = new GetIngredientByIdQuery(Guid.Empty);
            GetIngredientByIdQueryHandler handler = new GetIngredientByIdQueryHandler(_ingredientRepository.Object, _logger.Object);


            _ingredientRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()));
            
            Assert.ThrowsAsync<IngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetIngredientByIdQuery : with return")]
        public async Task GetIngredientByIdQueryTest_Ok_WithReturnAsync()
        {
            Guid guid = Guid.NewGuid();

            Guid foodTypeId = Guid.NewGuid();
            GetIngredientByIdQuery query = new GetIngredientByIdQuery(guid);
            GetIngredientByIdQueryHandler handler = new GetIngredientByIdQueryHandler(_ingredientRepository.Object, _logger.Object);

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

            _ingredientRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(ingredientReturn);

            GetIngredientByIdQueryResult result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Equal(ingredientReturn.Name, result.Name);
            Assert.Equal(ingredientReturn.Id, result.Id);
            Assert.Equal(ingredientReturn.FoodType.Name, result.FoodTypeName);
        }
    }
}
