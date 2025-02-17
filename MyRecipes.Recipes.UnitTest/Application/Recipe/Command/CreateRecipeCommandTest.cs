using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction;
using MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId;
using MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Repository.EF.Repository;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.UnitTest.Application.Recipe.Command
{
    public sealed class CreateRecipeCommandTest
    {
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<CreateRecipeCommandHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("CreateRecipeCommand : WrongParameterException ")]
        public void CreateRecipeCommandTest_WrongParameterException_id()
        {
            string name = null;
            Domain.Entity.Enum.Difficulty difficulty = Domain.Entity.Enum.Difficulty.Normal;
            int nbGuest = 1;
            int TimeToPrepareRecipe = 120;
            CreateRecipeCommand query = new CreateRecipeCommand(name, difficulty, TimeToPrepareRecipe, nbGuest);
            CreateRecipeCommandHandler handler = new CreateRecipeCommandHandler(_recipesRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("CreateRecipeCommand : Ok ")]
        public async void CreateRecipeCommandTest_Ok()
        {
            string name = "Banane flambée";
            Domain.Entity.Enum.Difficulty difficulty = Domain.Entity.Enum.Difficulty.Normal;
            int nbGuest = 1;
            int TimeToPrepareRecipe = 120;
            CreateRecipeCommand query = new CreateRecipeCommand(name, difficulty, TimeToPrepareRecipe, nbGuest);
            CreateRecipeCommandHandler handler = new CreateRecipeCommandHandler(_recipesRepository.Object, _logger.Object);
            _recipesRepository.Setup(s => s.AddAsync(It.IsAny<Domain.Entity.Recipe>()));
            await handler.Handle(query, _cancellationToken);
        }
    }
}
