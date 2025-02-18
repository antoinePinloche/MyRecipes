using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Exception
{
    public class UserRoleNotFoundException : ExceptionBase
    {
        public UserRoleNotFoundException(string error, string message) : base(error, message)
        {
        }
    }
}
