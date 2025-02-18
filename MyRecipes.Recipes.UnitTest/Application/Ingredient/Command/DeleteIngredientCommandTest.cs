using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Command.DeteleIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Ingredient.Command
{
    public sealed class DeleteIngredientCommandTest
    {
        private readonly Mock<IIngredientRepository> _ingredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<ILogger<DeleteIngredientCommandHandler>> _logger = new Mock<ILogger<DeleteIngredientCommandHandler>>();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteIngredientCommand : Wrong parameter exception with empty Name")]
        public async Task DeleteIngredientCommandTest_WrongParameterException_withEmpty_NameAsync()
        {
            DeleteIngredientCommand command = new DeleteIngredientCommand(Guid.Empty);
            DeleteIngredientCommandHandler handler = new DeleteIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("DeleteIngredientCommand : Wrong parameter exception with empty Id")]
        public async Task DeleteIngredientCommandTest_IngredientNotFoundExceptionAsync()
        {
            DeleteIngredientCommand command = new DeleteIngredientCommand(Guid.NewGuid());
            DeleteIngredientCommandHandler handler = new DeleteIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<IngredientNotFoundException>(async () => await handler.Handle(command, _cancellationToken));
        }

        [Fact]
        [Description("DeleteIngredientCommand : Wrong parameter exception with empty Id")]
        public async void DeleteIngredientCommandTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid foodTypeId = Guid.NewGuid();
            DeleteIngredientCommand command = new DeleteIngredientCommand(guid);
            DeleteIngredientCommandHandler handler = new DeleteIngredientCommandHandler(_ingredientRepository.Object, _logger.Object);

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
            _ingredientRepository.Setup(x => x.RemoveAsync(It.IsAny<Domain.Entity.Ingredient>()));
            await handler.Handle(command, _cancellationToken);
        }
    }
}
