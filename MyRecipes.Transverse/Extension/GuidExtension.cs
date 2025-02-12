using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Extension
{
    public static class GuidExtension
    {
        public static bool IsEmpty(this Guid guid)
        {
            if (guid == Guid.Empty)
                return true;
            return false;
        }

        public static bool IsNullOrEmpty(this Guid? guid)
        {
            if (guid is null)
                return true;
            if (guid == Guid.Empty)
                return true;
            return false;
        }
    }
}
