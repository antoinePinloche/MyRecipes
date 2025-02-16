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
        public string Error {  get; set; }
        public string Message {  get; set; }

        public ExceptionBase(string error, string message) : base(message)
        {
            Error = error;
            Message = message;
        }
    }
}
