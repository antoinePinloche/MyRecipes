﻿using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction
{
    /// <summary>
    /// Handler de la command <see cref="CreateListOfInstructionCommand"/>
    /// </summary>
    public class CreateListOfInstructionCommandHandler : IRequestHandler<CreateListOfInstructionCommand>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<CreateListOfInstructionCommandHandler> _logger;
        public CreateListOfInstructionCommandHandler(IInstructionRepository instructionRepository, ILogger<CreateListOfInstructionCommandHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _logger = logger;
        }

        public async Task Handle(CreateListOfInstructionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (request.Instructions.IsNullOrEmpty())
                {
                    throw new WrongParameterException(_logger,
                        nameof(Handle),
                        "CreateListOfInstructionCommandHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.INSTRUCTION);
                }
                var checkDuplicateRequest = request.Instructions.GroupBy(gb => gb.Step).Where(w => w.Count() > 1);
                foreach (var instruction in checkDuplicateRequest)
                {
                    if(instruction.ToList().GroupBy(g => g.RecipeId)
                        .Where(w => w.Count() > 1)
                        .Select(s => s.Key)
                        .Any())
                    {
                        throw new WrongParameterException(_logger,
                            nameof(Handle),
                            "CreateListOfInstructionCommandHandler",
                            Constant.EXCEPTION.TITLE.INSTRUCTION_DUPLICATION_CREATE,
                            Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.DUPLICATION_INSTRUCTION);
                    }
                }

                List<IGrouping<Guid?, CreateListOfInstructionCommand.Instruction>> list = request.Instructions.GroupBy(gb => gb.RecipeId).ToList();
                if (!list.IsNullOrEmpty())
                {
                    foreach(IGrouping<Guid?, CreateListOfInstructionCommand.Instruction> elem in list)
                    {
                        ICollection<Domain.Entity.Instruction> instructionList = await _instructionRepository.GetAllInstructionByRecipeIdAsync((Guid)elem.Key);
                        var intersect = elem.ToList().IntersectBy(instructionList.Select(s => s.Step),
                                                             ib => ib.Step);

                        if (intersect.Any())
                        {
                            throw new InstructionAlreadyExisteException(_logger,
                                nameof(Handle),
                                "CreateListOfInstructionCommandHandler",
                                Constant.EXCEPTION.TITLE.INSTRUCTION_DUPLICATION_CREATE,
                                $"Instruction {string.Join(", ", intersect.Select(s => s.Step))} already Exist");
                        }
                    }

                }
                List<Domain.Entity.Instruction> instructionsToAdd = request.Instructions.Select(s =>
                    new Domain.Entity.Instruction()
                    {
                        Id = Guid.NewGuid(),
                        RecipeId = s.RecipeId,
                        Step = s.Step,
                        StepName = s.StepName,
                        StepInstruction = s.StepInstruction
                    }).ToList();
                await _instructionRepository.AddRangeAsync(instructionsToAdd);
                _logger.LogInformation($"CreateListOfInstructionCommandHandler : List of instructions create");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
