﻿using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.UpdateInstruction
{
    /// <summary>
    /// Command pour modifier une instruction par son ID
    /// <see cref="UpdateInstructionCommandHandler"/>
    /// </summary>
    public class UpdateInstructionCommand : IRequest
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public UpdateInstructionCommand(Guid id, int step, string stepName, string stepInstruction)
        {
            Id = id;
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
