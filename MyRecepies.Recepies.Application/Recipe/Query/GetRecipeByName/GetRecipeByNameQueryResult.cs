﻿using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
    /// <summary>
    /// Resultat de la query <see cref="GetRecipeByNameQuery"/>
    /// </summary>
    public class GetRecipeByNameQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Domain.Entity.RecipeIngredient>? Ingredients { get; set; }
        public IEnumerable<Domain.Entity.Instruction>? Instructions { get; set; }
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }

        public GetRecipeByNameQueryResult(Guid id, string name, IEnumerable<Domain.Entity.RecipeIngredient>? ingredients, IEnumerable<Domain.Entity.Instruction>? instructions, Difficulty recipyDifficulty, int timeToPrepareRecipe, int nbGuest)
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
