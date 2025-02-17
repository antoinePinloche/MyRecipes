using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName;
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
    public sealed class GetIngredientByNameQueryTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<ILogger<GetIngredientByNameQueryHandler>> _logger = new Mock<ILogger<GetIngredientByNameQueryHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetIngredientByNameQuery : WrongParameterException")]
        public void GetIngredientByNameQueryTest_WrongParameterException_withEmpty_Name()
        {
            GetIngredientByNameQuery query = new GetIngredientByNameQuery("");
            GetIngredientByNameQueryHandler handler = new GetIngredientByNameQueryHandler(_ingredientRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetIngredientByNameQuery : IngredientNotFoundException")]
        public async void GetIngredientByNameQueryTest_IngredientNotFoundException()
        {
            GetIngredientByNameQuery query = new GetIngredientByNameQuery("Banane");
            GetIngredientByNameQueryHandler handler = new GetIngredientByNameQueryHandler(_ingredientRepository.Object, _logger.Object);


            _ingredientRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()));

            Assert.ThrowsAsync<IngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetIngredientByNameQuery : with return")]
        public async Task GetIngredientByNameQueryTest_Ok_WithReturnAsync()
        {
            Guid guid = Guid.NewGuid();

            Guid foodTypeId = Guid.NewGuid();
            GetIngredientByNameQuery query = new GetIngredientByNameQuery("Banane");
            GetIngredientByNameQueryHandler handler = new GetIngredientByNameQueryHandler(_ingredientRepository.Object, _logger.Object);

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

            GetIngredientByNameQueryResult result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Equal(ingredientReturn.Name, result.Name);
            Assert.Equal(ingredientReturn.Id, result.Id);
            Assert.Equal(ingredientReturn.FoodType.Name, result.FoodTypeName);
        }
    }
}
