using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryIngredient
{
    public interface IIngredientRepository : IRepository<Ingredient, Guid>
    {
    }
}
