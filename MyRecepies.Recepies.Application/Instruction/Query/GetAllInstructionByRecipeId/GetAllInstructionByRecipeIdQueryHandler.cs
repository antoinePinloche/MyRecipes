﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    public class GetAllInstructionByRecipeIdQueryHandler : IRequestHandler<GetAllInstructionByRecipeIdQuery, List<GetAllInstructionByRecipeIdQueryResult>>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ILogger<GetAllInstructionByRecipeIdQueryHandler> _logger;
        public GetAllInstructionByRecipeIdQueryHandler(IInstructionRepository instructionRepository, IRecipesRepository recipesRepository, ILogger<GetAllInstructionByRecipeIdQueryHandler> logger)
        {
            _recipesRepository = recipesRepository;
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task<List<GetAllInstructionByRecipeIdQueryResult>> Handle(GetAllInstructionByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                var recipeFound = await _recipesRepository.GetAsync(request.Id);
                if (recipeFound is null)
                {
                    throw new RecipeNotFoundException("Invalide Key", $"Recipe notfound with ID {request.Id}");
                }
                var entityFound = await _instructionRepository.GetAllInstructionByRecipeIdAsync(request.Id);
                if (entityFound.IsNullOrEmpty())
                    throw new InstructionNotFoundException("Invalide Key", $"Instruction for recipe {request.Id} not found");
                _logger.LogInformation($"GetAllInstructionByRecipeIdQueryHandler : instruction found for recipe {request.Id}");
                return entityFound.OrderBy(ob => ob.Step).Select(s =>
                    new GetAllInstructionByRecipeIdQueryResult(
                        s.Id,
                        s.Step,
                        s.StepName,
                        s.StepInstruction
                        )
                ).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
