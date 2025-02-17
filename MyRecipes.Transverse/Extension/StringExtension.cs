using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Extension
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            if (str is null)
                return true;
            if (str.Equals(string.Empty))
                return true;
            return false;
        }
    }
}
