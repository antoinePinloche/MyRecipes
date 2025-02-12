using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Recipe.Command.CreateRecipe
{
    public class CreateRecipeCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }

        public CreateRecipeCommand(string name, Difficulty recipyDifficulty, int timeToPrepareRecipe, int nbGuest)
        {
            Name = name;
            RecipyDifficulty = recipyDifficulty;
            TimeToPrepareRecipe = timeToPrepareRecipe;
            NbGuest = nbGuest;
        }
    }
}
