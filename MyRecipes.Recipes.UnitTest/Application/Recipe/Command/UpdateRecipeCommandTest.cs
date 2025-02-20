﻿using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Recipe.Command.UpdateRecipe;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Command
{
    public sealed class UpdateRecipeCommandTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<UpdateRecipeCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("UpdateRecipeCommand : WrongParameterException ")]
        public async Task UpdateRecipeCommandTest_WrongParameterException_idAsync()
        {
            Guid id = Guid.Empty;
            string Name = string.Empty;
            Difficulty RecipyDifficulty = Difficulty.Beginner;
            int TimeToPrepareRecipe = 120;
            int NbGuest = 1;


            UpdateRecipeCommand query = new UpdateRecipeCommand(id, Name, RecipyDifficulty, TimeToPrepareRecipe, NbGuest);
            UpdateRecipeCommandHandler handler = new UpdateRecipeCommandHandler(_recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateRecipeCommand : RecipeNotFoundException")]
        public async Task UpdateRecipeCommandTest_RecipeNotFoundExceptionAsync()
        {
            Guid id = Guid.NewGuid();
            string Name = "Name";
            Difficulty RecipyDifficulty = Difficulty.Beginner;
            int TimeToPrepareRecipe = 120;
            int NbGuest = 1;


            UpdateRecipeCommand query = new UpdateRecipeCommand(id, Name, RecipyDifficulty, TimeToPrepareRecipe, NbGuest);
            UpdateRecipeCommandHandler handler = new UpdateRecipeCommandHandler(_recipesRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("UpdateRecipeCommand : Ok")]
        public async Task UpdateRecipeCommandTest_Ok()
        {
            Guid id = Guid.NewGuid();
            string Name = "Name";
            Difficulty RecipyDifficulty = Difficulty.Beginner;
            int TimeToPrepareRecipe = 120;
            int NbGuest = 1;


            UpdateRecipeCommand query = new UpdateRecipeCommand(id, Name, RecipyDifficulty, TimeToPrepareRecipe, NbGuest);
            UpdateRecipeCommandHandler handler = new UpdateRecipeCommandHandler(_recipesRepository.Object, _logger.Object);

            _recipesRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                new Domain.Entity.Recipe()
                {
                    Id = id,
                    Name = "new Name",
                    RecipyDifficulty = RecipyDifficulty,
                    TimeToPrepareRecipe = 80,
                    NbGuest = NbGuest,
                    Ingredients = null,
                    Instructions = null
                });
            _recipesRepository.Setup(s => s.UpdateAsync(It.IsAny<Domain.Entity.Recipe>()));
            await handler.Handle(query, _cancellationToken);
        }
    }
}
