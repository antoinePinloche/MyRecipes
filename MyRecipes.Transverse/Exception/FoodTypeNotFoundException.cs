using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Exception
{
    public class FoodTypeNotFoundException : ExceptionBase
    {
        //public FoodTypeNotFoundException(string method, string sourcePath, string message) : base(method, sourcePath, message)
        //{
        //}
        public FoodTypeNotFoundException(string error, string message) : base(error, message)
        {
        }
    }
}
