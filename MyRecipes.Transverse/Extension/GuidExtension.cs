using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Extension
{
    /// <summary>
    /// Methode d'extension pour la structure Guid
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// permet de savoir si Guid est empty
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid guid)
        {
            if (guid == Guid.Empty)
                return true;
            return false;
        }
        /// <summary>
        /// permet de savoir si Guid? est null ou empty
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
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
