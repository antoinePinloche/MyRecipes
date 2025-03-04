﻿using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe
{
    /// <summary>
    /// Command pour crée une recette
    /// <see cref="CreateRecipeCommandHandler"/>
    /// </summary>
    public class CreateRecipeCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }
        public Guid UserId { get; set; }

        public CreateRecipeCommand(string name, Difficulty recipyDifficulty, int timeToPrepareRecipe, int nbGuest, Guid userId)
        {
            Name = name;
            RecipyDifficulty = recipyDifficulty;
            TimeToPrepareRecipe = timeToPrepareRecipe;
            NbGuest = nbGuest;
            UserId = userId;
        }
    }
}
