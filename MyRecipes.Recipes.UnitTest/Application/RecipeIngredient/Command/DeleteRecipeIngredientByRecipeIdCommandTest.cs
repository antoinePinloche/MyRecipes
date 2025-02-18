using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Command
{
    public sealed class DeleteRecipeIngredientByRecipeIdCommandTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<ILogger<DeleteRecipeIngredientByRecipeIdCommandHandler>> _logger = new();
        private readonly Mock<ISender> _sender = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteRecipeIngredientByRecipeIdCommand : WrongParameterException guid")]
        public async void DeleteRecipeIngredientByRecipeIdCommand_WrongParameterException_guid()
        {
            Guid guid = Guid.Empty;

            DeleteRecipeIngredientByRecipeIdCommand query = new DeleteRecipeIngredientByRecipeIdCommand(guid);

            DeleteRecipeIngredientByRecipeIdCommandHandler handler = new DeleteRecipeIngredientByRecipeIdCommandHandler(_recipeIngredientRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientByRecipeIdCommand : Ok")]
        public async void DeleteRecipeIngredientByRecipeIdCommand_Ok()
        {
            Guid guid = Guid.NewGuid();

            DeleteRecipeIngredientByRecipeIdCommand query = new DeleteRecipeIngredientByRecipeIdCommand(guid);

            DeleteRecipeIngredientByRecipeIdCommandHandler handler = new DeleteRecipeIngredientByRecipeIdCommandHandler(_recipeIngredientRepository.Object, _logger.Object);

            await handler.Handle(query, _cancellationToken);
        }
    }
}
