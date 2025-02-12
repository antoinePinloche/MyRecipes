using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById
{
    public class GetRecipeByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Domain.Entity.RecipeIngredient>? Ingredients { get; set; }
        public IEnumerable<Domain.Entity.Instruction>? Instructions { get; set; }
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }

        public GetRecipeByIdQueryResult(Guid id, string name, IEnumerable<Domain.Entity.RecipeIngredient>? ingredients, IEnumerable<Domain.Entity.Instruction>? instructions, Difficulty recipyDifficulty, int timeToPrepareRecipe, int nbGuest)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
            RecipyDifficulty = recipyDifficulty;
            TimeToPrepareRecipe = timeToPrepareRecipe;
            NbGuest = nbGuest;
        }
    }
}
