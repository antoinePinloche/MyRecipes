using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MyRecipes.Transverse.Exception
{
    public abstract class ExceptionBase : System.Exception
    {
        internal ExceptionBase() { }

        public ExceptionBase(string method, string sourcePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourcePath)}.{method} : {message}")
        {
        }
    }
}
