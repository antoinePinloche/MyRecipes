using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName;
using MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyRecipes.Recipes.UnitTest.Application.Instruction.Query
{
    public sealed class GetAllInstructionByRecipeIdQueryTest
    {
        private readonly Mock<IInstructionRepository> _instructionRepository = new();
        private readonly Mock<IRecipesRepository> _recipesRepository = new();
        private readonly Mock<ILogger<GetAllInstructionByRecipeIdQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetAllInstructionByRecipeIdQuery : WrongParameterException")]
        public void GetAllInstructionByRecipeIdQueryTest_WrongParameterException()
        {
            Guid guid = Guid.Empty;
            GetAllInstructionByRecipeIdQuery query = new GetAllInstructionByRecipeIdQuery(guid);
            GetAllInstructionByRecipeIdQueryHandler handler = new GetAllInstructionByRecipeIdQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetAllInstructionByRecipeIdQuery : RecipeNotFoundException")]
        public void GetAllInstructionByRecipeIdQueryTest_RecipeNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            GetAllInstructionByRecipeIdQuery query = new GetAllInstructionByRecipeIdQuery(guid);
            GetAllInstructionByRecipeIdQueryHandler handler = new GetAllInstructionByRecipeIdQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            Assert.ThrowsAsync<RecipeNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetAllInstructionByRecipeIdQuery : InstructionNotFoundException")]
        public void GetAllInstructionByRecipeIdQueryTest_InstructionNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeGuid = Guid.NewGuid();
            GetAllInstructionByRecipeIdQuery query = new GetAllInstructionByRecipeIdQuery(guid);
            GetAllInstructionByRecipeIdQueryHandler handler = new GetAllInstructionByRecipeIdQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            Domain.Entity.Recipe recipe = new Domain.Entity.Recipe() { Id = recipeGuid, Ingredients = null, Instructions = null, Name = "Boeuf Sauté", NbGuest = 1, RecipyDifficulty = Domain.Entity.Enum.Difficulty.Beginner, TimeToPrepareRecipe = 120 };
            _recipesRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(recipe);

            Assert.ThrowsAsync<InstructionNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetAllInstructionByRecipeIdQuery : Ok")]
        public async void GetAllInstructionByRecipeIdQueryTest_Ok()
        {
            Guid guid = Guid.NewGuid();
            Guid recipeGuid = Guid.NewGuid();
            GetAllInstructionByRecipeIdQuery query = new GetAllInstructionByRecipeIdQuery(guid);
            GetAllInstructionByRecipeIdQueryHandler handler = new GetAllInstructionByRecipeIdQueryHandler(_instructionRepository.Object, _recipesRepository.Object, _logger.Object);

            Domain.Entity.Recipe recipe = new Domain.Entity.Recipe() { Id = recipeGuid, Ingredients = null, Instructions = null, Name = "Boeuf Sauté", NbGuest = 1, RecipyDifficulty = Domain.Entity.Enum.Difficulty.Beginner, TimeToPrepareRecipe = 120 };
            _recipesRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(recipe);

            Domain.Entity.Instruction instruction = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 0, StepInstruction = "", StepName = "préparation" };
            Domain.Entity.Instruction instruction2 = new() { Id = Guid.NewGuid(), RecipeId = recipeGuid, Step = 1, StepInstruction = "", StepName = "préparation" };

            _instructionRepository.Setup(x => x.GetAllInstructionByRecipeIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entity.Instruction>() { instruction, instruction2
            });
            List<GetAllInstructionByRecipeIdQueryResult> result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }
    }
}
