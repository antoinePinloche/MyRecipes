using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Command.DeleteRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Command
{
    public sealed class DeleteRecipeCommandTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<DeleteRecipeCommandHandler>> _logger = new();
        private readonly Mock<ISender> _sender = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteRecipeCommand : WrongParameterException ")]
        public async Task DeleteRecipeCommandTest_WrongParameterException_idAsync()
        {
            Guid id = Guid.Empty;

            DeleteRecipeCommand query = new DeleteRecipeCommand(id);
            DeleteRecipeCommandHandler handler = new DeleteRecipeCommandHandler(_recipesRepository.Object, _sender.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }
        
        [Fact]
        [Description("DeleteRecipeCommand : RecipeNotFoundException ")]
        public async Task DeleteRecipeCommandTest_RecipeNotFoundExceptionAsync()
        {
            Guid id = Guid.NewGuid();

            DeleteRecipeCommand query = new DeleteRecipeCommand(id);
            DeleteRecipeCommandHandler handler = new DeleteRecipeCommandHandler(_recipesRepository.Object, _sender.Object, _logger.Object);

            await Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeCommand : RecipeNotFoundException ")]
        public async Task DeleteRecipeCommandTest_OkAsync()
        {
            Guid id = Guid.NewGuid();

            DeleteRecipeCommand query = new DeleteRecipeCommand(id);
            DeleteRecipeCommandHandler handler = new DeleteRecipeCommandHandler(_recipesRepository.Object, _sender.Object, _logger.Object);

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Domain.Entity.Recipe()
                {
                    Id = id,
                    RecipyDifficulty = 0,
                    Ingredients = null,
                    Instructions = null,
                    Name = "Banane flambée",
                    NbGuest = 0,
                    TimeToPrepareRecipe = 120
                });
            _recipesRepository.Setup(s => s.RemoveAsync(It.IsAny<Domain.Entity.Recipe>()));
            await handler.Handle(query, _cancellationToken);
        }
    }
}
