namespace MyRecipes.Recipes.Domain.Entity
{
    public class Instruction
    {
        public Guid Id { get; set; }
        public Guid? RecipeId { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;
    }
}
