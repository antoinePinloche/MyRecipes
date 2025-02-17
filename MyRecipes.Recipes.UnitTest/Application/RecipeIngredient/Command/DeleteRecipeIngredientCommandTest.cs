using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.CreateRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredient;
using MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.RecipeIngredient.Command
{
    public sealed class DeleteRecipeIngredientCommandTest
    {
        private readonly Mock<IRecipeIngredientRepository> _recipeIngredientRepository = new();
        private readonly Mock<ILogger<DeleteRecipeIngredientCommandHandler>> _logger = new();
        private readonly Mock<ISender> _sender = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteRecipeIngredientCommand : WrongParameterException guid")]
        public async void DeleteRecipeIngredientCommand_WrongParameterException_guid()
        {
            Guid guid = Guid.Empty;

            DeleteRecipeIngredientCommand query = new DeleteRecipeIngredientCommand(guid);

            DeleteRecipeIngredientCommandHandler handler = new DeleteRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : RecipeIngredientNotFoundException")]
        public async void DeleteRecipeIngredientCommand_RecipeIngredientNotFoundException()
        {
            Guid guid = Guid.NewGuid();

            DeleteRecipeIngredientCommand query = new DeleteRecipeIngredientCommand(guid);
            DeleteRecipeIngredientCommandHandler handler = new DeleteRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            _sender.Setup(s => s.Send(It.IsAny<GetRecipeIngredientByIdQuery>(), _cancellationToken));

            await Assert.ThrowsAsync<RecipeIngredientNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("DeleteRecipeIngredientCommand : Ok")]
        public async void DeleteRecipeIngredientCommand_Ok()
        {
            Guid guid = Guid.NewGuid();

            DeleteRecipeIngredientCommand query = new DeleteRecipeIngredientCommand(guid);
            DeleteRecipeIngredientCommandHandler handler = new DeleteRecipeIngredientCommandHandler(_recipeIngredientRepository.Object, _sender.Object, _logger.Object);

            _sender.Setup(s => s.Send(It.IsAny<GetRecipeIngredientByIdQuery>(), _cancellationToken))
                .ReturnsAsync(
                    new GetRecipeIngredientByIdQueryResult(
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        null,
                        1,
                        UnitOfMeasure.Kg,
                        Guid.NewGuid())
                );

            await handler.Handle(query, _cancellationToken);
        }
    }
}
