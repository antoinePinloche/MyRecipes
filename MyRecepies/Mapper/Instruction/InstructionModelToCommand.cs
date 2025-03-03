using MyRecipes.Web.API.Models.Class.Instruction.Model;
using Create = MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction;
using CreateList = MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction;
using Delete = MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction;
using DeleteByRecipeId = MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId;
using Update = MyRecipes.Recipes.Application.Instruction.Command.UpdateInstruction;

namespace MyRecipes.Web.API.Mapper.Instruction
{
    public static class InstructionModelToCommand
    {
        public static Create.CreateInstructionCommand ToCommand(this CreateInstructionModel command)
        {
            return new Create.CreateInstructionCommand(command.RecipeId, command.Step, command.StepName, command.StepInstruction);
        }

        public static CreateList.CreateListOfInstructionCommand ToCommand(this List<CreateInstructionModel> command)
        {
            return new CreateList.CreateListOfInstructionCommand(command.Select(c =>
                new CreateList.CreateListOfInstructionCommand.Instruction(
                    c.RecipeId,
                    c.Step,
                    c.StepName,
                    c.StepInstruction)
                ).ToList());
        }

        public static Delete.DeleteInstructionCommand ToDeleteInstructionCommand(this Guid id)
        {
            return new Delete.DeleteInstructionCommand(id);
        }

        public static DeleteByRecipeId.DeleteInstructionByRecipeIdCommand ToDeleteByRecipeCommand(this Guid id)
        {
            return new DeleteByRecipeId.DeleteInstructionByRecipeIdCommand(id);
        }

        public static Update.UpdateInstructionCommand ToCommand(this UpdateInstructionModel command, Guid id)
        {
            return new Update.UpdateInstructionCommand(id, command.Step, command.StepName, command.StepInstruction);
        }
    }
}
