﻿using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Ingredient.Query
{
    public sealed class GetAllIngredientQueryTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<ILogger<GetAllIngredientQueryHandler>> _logger = new Mock<ILogger<GetAllIngredientQueryHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetAllIngredientQuery : with return")]
        public async Task GetAllIngredientQueryTest_Ok_WithReturnAsync()
        {
            Guid guid = Guid.NewGuid();

            Guid foodTypeId = Guid.NewGuid();
            GetAllIngredientQuery query = new GetAllIngredientQuery();
            GetAllIngredientQueryHandler handler = new GetAllIngredientQueryHandler(_ingredientRepository.Object, _logger.Object);

            Domain.Entity.Ingredient ingredientReturn1 = new Domain.Entity.Ingredient()
            {
                Id = guid,
                FoodType = new Domain.Entity.FoodType()
                {
                    Id = foodTypeId,
                    Name = "Fruit"
                },
                FoodTypeId = foodTypeId,
                Name = "Pomme"
            };

            Domain.Entity.Ingredient ingredientReturn2 = new Domain.Entity.Ingredient()
            {
                Id = guid,
                FoodType = new Domain.Entity.FoodType()
                {
                    Id = foodTypeId,
                    Name = "Fruit"
                },
                FoodTypeId = foodTypeId,
                Name = "Banane"
            };

            _ingredientRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Domain.Entity.Ingredient>() { ingredientReturn1, ingredientReturn2 });
            List<GetAllIngredientQueryResult> result = await handler.Handle(query, _cancellationToken); 
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        [Description("GetAllIngredientQuery : with Empty return")]
        public async Task GetAllIngredientQueryTest_Ok_WithEmptyReturnAsync()
        {
            Guid guid = Guid.NewGuid();

            Guid foodTypeId = Guid.NewGuid();
            GetAllIngredientQuery query = new GetAllIngredientQuery();
            GetAllIngredientQueryHandler handler = new GetAllIngredientQueryHandler(_ingredientRepository.Object, _logger.Object);

            _ingredientRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Domain.Entity.Ingredient>() {});
            List<GetAllIngredientQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
