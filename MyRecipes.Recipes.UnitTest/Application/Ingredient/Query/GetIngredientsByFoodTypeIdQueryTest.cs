using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId;
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
    public sealed class GetIngredientsByFoodTypeIdQueryTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<IFoodTypeRepository> _foodTypeRepository = new Mock<IFoodTypeRepository>();
        private readonly Mock<ILogger<GetIngredientsByFoodTypeIdQueryHandler>> _logger = new Mock<ILogger<GetIngredientsByFoodTypeIdQueryHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetIngredientsByFoodTypeIdQuery : WrongParameterException")]
        public void GetIngredientsByFoodTypeIdQueryTest_WrongParameterException_withEmpty_Name()
        {
            GetIngredientsByFoodTypeIdQuery query = new GetIngredientsByFoodTypeIdQuery(Guid.Empty);
            GetIngredientsByFoodTypeIdQueryHandler handler = new GetIngredientsByFoodTypeIdQueryHandler(_ingredientRepository.Object, _foodTypeRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetIngredientsByFoodTypeIdQuery : FoodTypeNotFoundException")]
        public async void GetIngredientsByFoodTypeIdQueryTest_FoodTypeNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            GetIngredientsByFoodTypeIdQuery query = new GetIngredientsByFoodTypeIdQuery(guid);
            GetIngredientsByFoodTypeIdQueryHandler handler = new GetIngredientsByFoodTypeIdQueryHandler(_ingredientRepository.Object, _foodTypeRepository.Object, _logger.Object);


            _ingredientRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()));

            Assert.ThrowsAsync<FoodTypeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetIngredientsByFoodTypeIdQuery : with return")]
        public async Task GetIngredientsByFoodTypeIdQueryTest_Ok_WithReturnAsync()
        {
            Guid guid = Guid.NewGuid();

            Guid foodTypeId = Guid.NewGuid();
            GetIngredientsByFoodTypeIdQuery query = new GetIngredientsByFoodTypeIdQuery(guid);
            GetIngredientsByFoodTypeIdQueryHandler handler = new GetIngredientsByFoodTypeIdQueryHandler(_ingredientRepository.Object, _foodTypeRepository.Object, _logger.Object);

            Domain.Entity.FoodType foodType = new Domain.Entity.FoodType()
            {
                Id = foodTypeId,
                Name = "Fruit"
            };

            Domain.Entity.Ingredient ingredientReturn = new Domain.Entity.Ingredient()
            {
                Id = guid,
                FoodType = foodType,
                FoodTypeId = foodTypeId,
                Name = "Already"
            };
            Domain.Entity.Ingredient ingredientReturn2 = new Domain.Entity.Ingredient()
            {
                Id = guid,
                FoodType = foodType,
                FoodTypeId = foodTypeId,
                Name = "Already"
            };

            _foodTypeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(foodType);
            _ingredientRepository.Setup(x => x.GetAllIngredientsByFoodTypeId(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Ingredient>() { ingredientReturn, ingredientReturn2 });

            List<GetIngredientsByFoodTypeIdQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
