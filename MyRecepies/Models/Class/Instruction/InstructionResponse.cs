namespace MyRecipes.Web.API.Models.Class.Instruction
{
    public class InstructionResponse
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public string StepDisplayName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;


        public InstructionResponse() { }
        public InstructionResponse(Guid guid, int step, string stepDisplayName, string stepInstruction)
        {
            Id = guid;
            Step = step;
            StepDisplayName = stepDisplayName;
            StepInstruction = stepInstruction;
        }
    }
}
